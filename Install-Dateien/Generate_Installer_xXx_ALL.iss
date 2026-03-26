#define MyAppName "IHKunterxIHKunter Lernspiel"
#define MyAppVersion "1.0.0"
#define MyAppPublisher "Team xXx"
#define MyAppURL "<https://github.com/SamerAntar/xXx-Project>"
#define FolderName "publish_MyOwnApp"

[Setup] 
AppId = {{D90B47F8-7EDD-40FF-A21C-980A13AB8642}
AppName= {#MyAppName}
AppVersion= {#MyAppVersion}
AppPublisher = {#MyAppPublisher}
AppPublisherURL = {#MyAppURL}
AppSupportURL = {#MyAppURL}
AppUpdatesURL = {#MyAppURL}
DefaultDirName={autopf}\{#MyAppName}
DefaultGroupName={#MyAppName}
PrivilegesRequired = lowest
OutputDir=Installer
OutputBaseFilename=Setup 
SolidCompression = yes
WizardStyle = modern
SetupIconFile = app.ico
 
[Files] 
Source: "..\Schulprojekt\bin\Release\net8.0\{#FolderName}\*"; DestDir: "{app}\xXxSchulprojekt"; Flags: recursesubdirs createallsubdirs
Source: "xXx_lernspieldb.sql"; DestDir: "{app}" 
Source: "app.ico" ; DestDir: "{app}"
Source: "readme.md" ; DestDir: "{app}"; Flags: isreadme

 
[Icons] 
Name: "{autoprograms}\{#MyAppName}"; Filename: "{app}\xXxSchulprojekt\Schulprojekt.exe"; WorkingDir: "{app}\xXxSchulprojekt"; IconFilename:"{app}\app.ico"
Name: "{autodesktop}\{#MyAppName}"; Filename: "{app}\xXxSchulprojekt\Schulprojekt.exe"; WorkingDir: "{app}\xXxSchulprojekt"; IconFilename:"{app}\app.ico"
[Run] 
Filename: "{app}\xXxSchulprojekt\Schulprojekt.exe"; Description: "Starte Anwendung {#MyAppName}"; Flags: nowait postinstall
 
[Code] 
 var
  importDB: Boolean;
  
function IsXamppInstalled(): Boolean;
begin
  Result := DirExists('C:\xampp');
end;

function CheckDatabaseServer(): Boolean;
var
  ResultCode: Integer;
  ExecResult: Boolean;
begin
  ExecResult := Exec(
    'C:\xampp\mysql\bin\mysql.exe',
    '-u root -e "SELECT 1"',
    'C:\xampp\mysql\bin',
    SW_HIDE,
    ewWaitUntilTerminated,
    ResultCode
  );

  if not ExecResult then
  begin
    MsgBox('mysqladmin konnte nicht gestartet werden (Pfad oder Rechteproblem).', mbError, MB_OK);
    Result := False;
  end
  else if ResultCode <> 0 then
  begin
    MsgBox('MySQL antwortet nicht (läuft evtl. nicht oder Zugriff verweigert).', mbError, MB_OK);
    Result := False;
  end
  else
    Result := True;
end;

function CheckDatabaseExists(DBName: String): Boolean;
var
  ResultCode: Integer;
begin
  Result := Exec('C:\xampp\mysql\bin\mysql.exe', '-u root -e "USE ' + DBName + ';"', 'C:\xampp\mysql\bin', SW_HIDE, ewWaitUntilTerminated, ResultCode) and (ResultCode = 0);
end;

procedure ImportDatabase(SQLFile: String);
var
  BatchFile: String;
  BatchContent: String;
  ResultCode: Integer;
  MySQLPassword: String;
begin
  // Optional: Passwort für root-User angeben STandart ''
  MySQLPassword := '';

  BatchFile := ExpandConstant('{tmp}\importdb.bat');
  // Batch-Datei erstellen
  if MySQLPassword = '' then
    BatchContent := '@echo off' + #13#10 +
                    'cd /d C:\xampp\mysql\bin' + #13#10 +
                    'mysql -u root -e "CREATE DATABASE IF NOT EXISTS lernspieldb;"' + #13#10 +
                    'mysql -u root lernspieldb < "' + SQLFile + '"' + #13#10 +
                    'exit'
  else
    BatchContent := '@echo off' + #13#10 +
                    'cd /d C:\xampp\mysql\bin' + #13#10 +
                    'mysql -u root -e "CREATE DATABASE IF NOT EXISTS lernspieldb;"' + #13#10 +
                    'mysql -u root -p' + MySQLPassword + ' lernspieldb < "' + SQLFile + '"' + #13#10 +
                    'exit';
  // Batch-Datei schreiben
  SaveStringToFile(BatchFile, BatchContent, False);
  // Batch ausführen
  Exec(BatchFile, '', '', SW_HIDE, ewWaitUntilTerminated, ResultCode);
  if ResultCode = 0 then
    MsgBox('Datenbank erfolgreich importiert.', mbInformation, MB_OK)
  else
    MsgBox('Fehler beim Import der Datenbank.', mbError, MB_OK);
end;

procedure InitializeWizard();
begin
  importDB := false //default kein import
  if not IsXamppInstalled() then
  begin
    MsgBox('XAMPP wurde nicht gefunden, wird jedoch benötigt. Bitte installieren sie zuerst Xampp oder stellen sie sicher, dass MySQL unter Port 3306 erreichbar ist.', mbError, MB_OK)
    exit;
  end;

  if CheckDatabaseServer() then
  begin
    if CheckDatabaseExists('lernspieldb') then
    begin
      if (MsgBox('Es wurde die Datenbank "lernspieldb" gefunden. Möchten Sie die Datenbank "lernspieldb" jetzt neu importieren? Dabei wird die alte Datenbank überschrieben!', mbConfirmation, MB_YESNO) = IDYES) then 
      begin
        importDB := True;
        MsgBox('Datenbank wird nach der Installation importiert. Das Spiel wird nun installiert.', mbInformation, MB_OK)
      end
      else
        MsgBox('Datenbank "lernspieldb" wird nicht neu importiert. Das Spiel wird nun installiert.', mbInformation, MB_OK);
    end
    else
    begin
      if (MsgBox('Die Datenbank "lernspieldb" muss jetzt importiert werden. Falls sie jetzt abbrechen, wird die gesamte Installation abgebrochen.', mbConfirmation, MB_YESNO) = IDYES) then
      begin
        importDB := True;
      end
      else
      begin
        MsgBox('Datenbank "lernspieldb" wird für die Anwendung benötigt. Installation wird abgebrochen.', mbError, MB_OK);
        exit;
      end;
    end;
  end
  else
  begin
    MsgBox('Datenbankserver nicht erreichbar. Bitte XAMPP starten.', mbError, MB_OK);
    exit;
  end;
end;

  procedure CurStepChanged(CurStep: TSetupStep);
    begin
      if CurStep = ssPostInstall then
      begin
        if importDB then
        begin
          ImportDatabase(ExpandConstant('{app}\xXx_lernspieldb.sql'));
        end;
      end;
    end;
 
