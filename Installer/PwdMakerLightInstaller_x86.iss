; -- PwdMakerLightInstaller.iss --
[Setup]
AppName=PwdMakerLight
AppId=Growup_Consultant/Software/PwdMakerLight
AppPublisher=Growup Consultant
AppCopyright=Growup Consultant
AppVerName=PwdMakerLight 1.0.0.0
AppVersion=1.0.0.0
ArchitecturesAllowed=x86 x64
DefaultDirName={pf}\PwdMakerLight
DefaultGroupName=PwdMakerLight
UninstallDisplayIcon={app}\PwdMakerLight.exe
ShowLanguageDialog=no
VersionInfoVersion=1.0.0.0
VersionInfoDescription=PwdMakerLightセットアッププログラム

OutputBaseFilename=PwdMakerLightInstaller_x86
OutputDir="."
SetupIconFile="..\PwdMakerLight\PwdMakerLight.ico"

[Tasks]
Name: desktopicon; Description: "デスクトップにショートカットアイコンを作成する";

[Files]
Source: "..\PwdMakerLight\bin\Release\PwdMakerLight.exe";    DestDir: "{app}"; Flags: ignoreversion
Source: "..\PwdMakerLight\bin\Release\PwdMakerLight.config"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\PwdMakerLight\bin\Release\NexFx.dll";            DestDir: "{app}"; Flags: ignoreversion

[Icons]
Name: "{group}\PwdMakerLight";         Filename: "{app}\PwdMakerLight.exe"
Name: "{commondesktop}\PwdMakerLight"; Filename: "{app}\PwdMakerLight.exe"; WorkingDir: "{app}"; Tasks: desktopicon

[Languages]
Name: japanese; MessagesFile: compiler:Languages\Japanese.isl

[Run]
Filename: "{app}\PwdMakerLight.exe"; Description: "アプリケーションを起動する"; Flags: postinstall shellexec skipifsilent
