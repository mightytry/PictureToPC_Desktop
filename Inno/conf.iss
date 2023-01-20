#define Version "0.8.0"
; Increment by one
#define Update 1
; Set to current Version if Debs update
#define UpdateDeps 1     

#define Name "PictureToPC"
#define Publisher "Mees Studio"
#define URL "https://github.com/mightytry/PictureToPC_Desktop/releases/tag/latest"
#define ExeName "PictureToPC.exe"

[Setup]
; NOTE: The value of AppId uniquely identifies this application. Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{82744DD3-2F6B-485C-8BD6-849B093FCE58}
AppName={#Name}
AppVersion={#Version}
AppVerName={#Name}
AppPublisher={#Publisher}
AppPublisherURL={#URL}
AppSupportURL={#URL}
AppUpdatesURL={#URL}
DefaultDirName={autopf}\{#Publisher}\{#Name}
DisableProgramGroupPage=yes
; Uncomment the following line to run in non administrative install mode (install for current user only.)
;PrivilegesRequired=lowest
PrivilegesRequiredOverridesAllowed=commandline
OutputBaseFilename=Installer v{#Version}
Compression=lzma
SolidCompression=yes
WizardStyle=modern

[Languages]
Name: "deutsch"; MessagesFile: "compiler:Languages/German.isl"
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
Source: "Z:\Cloud\Programieren\Github\PictureToPC_Desktop\PictureToPC\bin\Release\net6.0-windows\{#ExeName}"; DestDir: "{app}"; Flags: ignoreversion
Source: "Z:\Cloud\Programieren\Github\PictureToPC_Desktop\PictureToPC\bin\Release\net6.0-windows\PictureToPC.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "Z:\Cloud\Programieren\Github\PictureToPC_Desktop\PictureToPC\bin\Release\net6.0-windows\PictureToPC.runtimeconfig.json"; DestDir: "{app}"; Flags: ignoreversion
Source: "7za.exe"; DestDir: "{tmp}"; Flags: deleteafterinstall; Check: did_Download
Source: "{tmp}\x64\*"; DestDir: "{app}"; Flags: external recursesubdirs; Check: did_Download; BeforeInstall: Extract
; NOTE: Don't use "Flags: ignoreversion" on any shared system files

;[UninstallDelete]
;Type: filesandordirs; Name: "{app}\Emgu.CV.*";
;Type: filesandordirs; Name: "{app}\Newtonsoft.Json.dll";
;Type: filesandordirs; Name: "{app}\PictureToPC.*";
;Type: filesandordirs; Name: "{app}\x64\.*";

[Icons]
Name: "{autoprograms}\{#Name}"; Filename: "{app}\{#ExeName}"
Name: "{autodesktop}\{#Name}"; Filename: "{app}\{#ExeName}"; Tasks: desktopicon

[Registry]
Root: "HKLM"; Subkey: "SOFTWARE\{#Publisher}\{#Name}"; ValueType: dword; ValueName: "Version"; ValueData: {#Update}; 

[Run]
Filename: "{app}\{#ExeName}"; Description: "{cm:LaunchProgram,{#StringChange(Name, '&', '&&')}}"; Flags: nowait postinstall skipifsilent


[Code]
var
  DownloadPage: TDownloadWizardPage;
  Downloaded: Boolean;
  ResultCode: Integer;
  Version: Cardinal;

function OnDownloadProgress(const Url, FileName: String; const Progress, ProgressMax: Int64): Boolean;
begin
  if (Progress = ProgressMax) then
    Log(Format('Successfully downloaded file to {tmp}: %s', [FileName]));
    Result := True;
    Downloaded := True;
end;

procedure InitializeWizard;
begin
  DownloadPage := CreateDownloadPage(SetupMessage(msgWizardPreparing), SetupMessage(msgPreparingDesc), @OnDownloadProgress);
end;

procedure Extract;
begin
  Exec(ExpandConstant('{tmp}\7za.exe'), ExpandConstant('x ""{tmp}\x64.zip"" -o""{tmp}\x64\"" * -r -aoa'), '',  SW_HIDE, ewWaitUntilTerminated, ResultCode);
end;

function did_Download(): Boolean;
begin
  if(Downloaded) then begin
    Result:=True;
  end else
    Result:=False;
end;

function NextButtonClick(CurPageID: Integer): Boolean;
begin
  if (CurPageID = wpReady) and (not (DirExists(ExpandConstant('{app}\x64'))) or (not RegQueryDWordValue(HKEY_LOCAL_MACHINE, 'SOFTWARE\{#Publisher}\{#Name}', 'Version', Version)) or (Version < {#UpdateDeps})) then begin
    DownloadPage.Clear;
    DownloadPage.Add('https://github.com/mightytry/PictureToPC_Desktop/releases/download/deps/x64.zip', 'x64.zip', '');
    DownloadPage.Show;
    try
      try
        DownloadPage.Download; // This downloads the files to {tmp}
        Result := True;
      except
        if DownloadPage.AbortedByUser then
          Log('Aborted by user.')
        else
          SuppressibleMsgBox(AddPeriod(GetExceptionMessage), mbCriticalError, MB_OK, IDOK);
        Result := False;
      end;
    finally
      DownloadPage.Hide;
    end;
  end else
    Result := True;
end;