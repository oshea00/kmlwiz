; KMLWpf.nsi
; It will install KMLWpf.nsi into a directory that the user selects,

;--------------------------------

; The name of the installer
Name "KML Generator"

; The file to write
OutFile "KMLWpf.exe"

; The default installation directory
InstallDir "$PROGRAMFILES\Oshea00\KMLWpf"

; Registry key to check for directory (so if you install again, it will 
; overwrite the old one automatically)
InstallDirRegKey HKLM "Software\NSIS_KMLWpf" "Install_Dir"

;--------------------------------

; Pages

Page components
Page directory
Page instfiles

UninstPage uninstConfirm
UninstPage instfiles

;--------------------------------

; The stuff to install
Section "KML Generator (required)"

  SectionIn RO
  
  ; Set output path to the installation directory.
  SetOutPath $INSTDIR
  
  ; Put file there
  File "KMLWpf\bin\Debug\KMLWpf.exe"
  File "KMLWpf\bin\Debug\KMLWpf.exe.config"
  File "KMLWpf\bin\Debug\temp.KML"
  File "KMLWpf\KML.ico"
  
  ; Write the installation path into the registry
  WriteRegStr HKLM SOFTWARE\NSIS_KMLWpf "Install_Dir" "$INSTDIR"
  
  ; Write the uninstall keys for Windows
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\KMLWpf" "DisplayName" "KML Generator"
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\KMLWpf" "UninstallString" '"$INSTDIR\uninstall.exe"'
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\KMLWpf" "DisplayVersion" "1.0"
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\KMLWpf" "DisplayIcon" '"$INSTDIR\KML.ico"'
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\KMLWpf" "Publisher" "Mike O"
  WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\KMLWpf" "VersionMajor" 1
  WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\KMLWpf" "VersionMinor" 0
  WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\KMLWpf" "NoModify" 1
  WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\KMLWpf" "NoRepair" 1
  WriteUninstaller "uninstall.exe"
  
SectionEnd

; Optional section (can be disabled by the user)
Section "Start Menu Shortcuts"

  CreateDirectory "$SMPROGRAMS\KMLWpf"
  CreateShortcut "$SMPROGRAMS\KMLWpf\Uninstall.lnk" "$INSTDIR\uninstall.exe" "" "$INSTDIR\uninstall.exe" 0
  CreateShortcut "$SMPROGRAMS\KMLWpf\KML Generator.lnk" "$INSTDIR\KMLWpf.exe" "" "$INSTDIR\KML.ico" 0
  CreateShortcut "$DESKTOP\KML Generator.lnk" "$INSTDIR\KMLWpf.exe" "" "$INSTDIR\KML.ico" 0
  
SectionEnd

;--------------------------------

; Uninstaller

Section "Uninstall"
  
  ; Remove registry keys
  DeleteRegKey HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\KMLWpf"
  DeleteRegKey HKLM SOFTWARE\NSIS_KMLWpf

  ; Remove files and uninstaller
  Delete $INSTDIR\KMLWpf.exe"
  Delete $INSTDIR\KMLWpf.exe.config"
  Delete $INSTDIR\temp.KML"
  Delete $INSTDIR\KML.ico"
  Delete $INSTDIR\uninstall.exe

  ; Remove shortcuts, if any
  Delete "$SMPROGRAMS\KMLWpf\*.*"
  Delete "$DESKTOP\KML Generator.lnk"

  ; Remove directories used
  RMDir "$SMPROGRAMS\KMLWpf"
  RMDir "$INSTDIR"

SectionEnd
