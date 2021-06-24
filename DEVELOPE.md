## 개발관련 사항

### 개발 환경

Unity 및 .NET 으로 제작되었습니다.  
 -  Unity : 기본 화면 구성 및 랜덤 배정 등의 기능을 위해 사용되었습니다. 
 -  .NET : 결과 이미지를 클립보드에 설정하기 위해 .NET이 사용되었습니다. .NET 프로젝트는 별도로 있으며 본 레파지토리에는 [빌드파일](https://github.com/leehs27/RandomJobAssigner/blob/v1.0/Assets/UnityDataReceiver.exe)만 포함되어있습니다.
결과 이미지를 클립보드에 설정하기 위해 .NET이 사용되었습니다

Unity Version : 2020.3.3f1  
.NET Framework : .NET Framework 4.8

### 데이터 관리하기

<div align=center>
  <img src="https://github.com/leehs27/RandomJobAssigner/blob/v1.0/Image/ClassDiagram.png" alt="클래스 다이어그램"/>
  <br/>
  <sub>클래스 다이어그램</sub>
</div>
<br/>

Job, Person, Result는 Data라는 추상 클래스를 상속받습니다.  
각 Data마다 해당 Data를 가지고 있는 Monobehaviour 클래스인 DataItem이 존재합니다.  
각 DataItem마다 해당 DataItem List를 관리하고 화면에 나타내는 DataController 클래스가 구현되어 있습니다.  

### 에디터로 쉽게 보기

<div align=center>
  <img src="https://github.com/leehs27/RandomJobAssigner/blob/main/Image/EditorPath.png"alt="에디터 경로"/>
  <br/>
  <sub>데이터 세팅 여는 법</sub>
  <br/>
  <img src="https://github.com/leehs27/RandomJobAssigner/blob/main/Image/DataSetting.png"alt="에디터"/>
  <br/>
  <sub>데이터 세팅 윈도우</sub>
</div>
<br/>

에디터에는 해당 데이터를 보다 쉽게 확인하기 위해서 에디터 윈도우를 사용했습니다.  
상단 메뉴에서 Window/Datas를 눌러 해당 창을 열 수 있습니다.  
해당 창을 열기 위해서 [`ScriptableWizard` 클래스](https://docs.unity3d.com/kr/current/ScriptReference/ScriptableWizard.html)를 사용했습니다.   
관련된 코드는 [코드 파일](https://github.com/leehs27/RandomJobAssigner/blob/main/Assets/Scripts/Editor/DataWindow.cs)에서 직접 보실 수 있습니다.

### 캡처하기 

현재 화면을 캡처하는 코드는 아래와 같습니다.
```c#
// renderRect는 캡처하고자 하는 영역에 대한 Rect 타입의 데이터입니다.
Texture2D texture = new Texture2D((int)renderRect.width, (int)renderRect.height);
texture.ReadPixels(renderRect, 0, 0, false);
texture.Apply();
```

다만 이 코드를 사용하실 때 주의 사항은 모든 Render가 끝난 후 실행되어야 한다는 것입니다. <sub>[유니티 생명주기 참고](https://docs.unity3d.com/kr/current/Manual/ExecutionOrder.html)</sub>  
따라서 `Update`에서 실행되면 안되고, `End of Frame`에서 실행되어야 합니다. 
저는 `Coroutine`을 활용했습니다.

```c#
public void Capture()
{
    StartCoroutine(CaptureCoroutine());
}

private IEnumerator CaptureCoroutine()
{
    // 씬 렌더링이 완료 될 때 까지 대기합니다.
    yield return new WaitForEndOfFrame();
    
    // 렌더링이 완료된 후 캡처를 합니다.
    CopyToClipboard(ConvertToTexture2D(GetRectFromRectTransform(targetRectTransform)));
}
```

### 클립보드로 복사하기

클립보드로 복사하기 위해서는 `System.Windows.Forms` 네임스페이스의 `Clipboard.SetImage` 함수를 사용해야 합니다.  
유니티에서는  `System.Windows.Forms` 네임스페이스를 포함하고 있지 않기 때문에 .NET 프로그램으로 텍스처 정보를 전달한 뒤, .NET 프로그램에서 클립보드에 설정해줍니다. 
클립보드에 이미지를 설정하는 방법은 [관련 블로그 글](https://leehs27.github.io/programming/2021-03-29-ClipboardImageSaver/)과 [레파지토리](https://github.com/leehs27/ClipboardImageSaver)를 확인해주시기 바랍니다.  


먼저, 유니티에서 캡처한 Texture2D 데이터를 JPG로 변경해 파일에 저장합니다. 그리고 해당 파일의 경로를 인자로 .NET 프로그램을 실행시켜줍니다.
```c#
// JPG로 변환합니다.
byte[] bytes = screenshot.EncodeToJPG();
// JPG로 변환한 Byte 배열을 string으로 바꿉니다.
string byteString = Convert.ToBase64String(bytes);
// 변환한 string을 파일에 적습니다.
File.WriteAllText(Path.Combine(Application.dataPath, textureDataFileName.name + ".txt"), byteString);

// 파일의 경로를 인자로 .NET 프로세스를 실행합니다.
Process.Start(Path.Combine(Application.dataPath, unityDataReceiver.name + ".exe"), 
  $"\"{Path.Combine(Application.dataPath, textureDataFileName.name + ".txt")}\"");
```

.NET 프로세스에서는 인자로 받은 파일을 읽어 이미지로 변환합니다. 변환한 이미지를 클립보드에 설정합니다.
```c#
private static void Main(string[] args)
{
    if(args.Length > 0)
    {
        string imageString = File.ReadAllText(args[0]);
        byte[] imageBytes = Convert.FromBase64String(imageString);
        MemoryStream imageStream = new MemoryStream(imageBytes.Length);
        imageStream.Write(imageBytes, 0, imageBytes.Length);
        Image image = Image.FromStream(imageStream);
        Clipboard.SetImage(image);
        imageStream.Close();
        imageStream.Dispose();
        image.Dispose();
    }
}
```





