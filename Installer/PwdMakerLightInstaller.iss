; -- PwdMakerLightInstaller.iss --
[Setup]
AppName=PwdMakerLight
AppId=Growup_Consultant/Software/PwdMakerLight
AppPublisher=Growup Consultant
AppCopyright=Growup Consultant
AppVerName=PwdMakerLight 1.0.0.0
AppVersion=1.0.0.0
ArchitecturesInstallIn64BitMode=x64
ArchitecturesAllowed=x64
DefaultDirName={pf}\PwdMakerLight
DefaultGroupName=PwdMakerLight
UninstallDisplayIcon={app}\PwdMakerLight.exe
ShowLanguageDialog=no
VersionInfoVersion=1.0.0.0
VersionInfoDescription=PwdMakerLight�Z�b�g�A�b�v�v���O����

OutputBaseFilename=PwdMakerLightInstaller
OutputDir="."
SetupIconFile="..\PwdMakerLight\PwdMakerLight.ico"

[Tasks]
Name: desktopicon; Description: "�f�X�N�g�b�v�ɃV���[�g�J�b�g�A�C�R�����쐬����";

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
Filename: "{app}\PwdMakerLight.exe"; Description: "�A�v���P�[�V�������N������"; Flags: postinstall shellexec skipifsilent
