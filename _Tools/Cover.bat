cd C:\Apps\JetBrains.dotCover\


Echo : eWolfPodcasterCore
dotcover cover  /TargetExecutable="C:\Apps\NUnit.Console-3.10.0\bin\net35\nunit3-console.exe" /TargetArguments="C:\GitHub\eWolfApps\PodCaster\eWolfPodcasterUnitTests\bin\x64\Debug\eWolfPodcasterCoreUnitTests.dll" /Output="C:\GitHub\eWolfApps\Reports\eWolfPodcasterCore.html" /ReportType="HTML"

Echo : eWolfCommon
dotcover cover  /TargetExecutable="C:\Apps\NUnit.Console-3.10.0\bin\net35\nunit3-console.exe" /TargetArguments="C:\GitHub\eWolfApps\Common\eWolfCommonUnitTests\bin\Debug\eWolfCommonUnitTests.dll" /Output="C:\GitHub\eWolfApps\Reports\eWolfCommon.html" /ReportType="HTML"


Echo : RenameAllFilesInFolder
dotcover cover  /TargetExecutable="C:\Apps\NUnit.Console-3.10.0\bin\net35\nunit3-console.exe" /TargetArguments="C:\GitHub\eWolfApps\Commands\RenameAllFilesInFolderUnitTests\bin\Debug\RenameAllFilesInFolderUnitTests.dll" /Output="C:\GitHub\eWolfApps\Reports\RenameAllFilesInFolder.html" /ReportType="HTML"



Echo : IncrementUnitTests
dotcover cover  /TargetExecutable="C:\Apps\NUnit.Console-3.10.0\bin\net35\nunit3-console.exe" /TargetArguments="C:\GitHub\eWolfApps\Commands\IncrementUnitTests\bin\Debug\IncrementUnitTests.dll" /Output="C:\GitHub\eWolfApps\Reports\Increment.html" /ReportType="HTML"



Echo : SystemTrayTools
dotcover cover  /TargetExecutable="C:\Apps\NUnit.Console-3.10.0\bin\net35\nunit3-console.exe" /TargetArguments="C:\GitHub\eWolfApps\SystemTray\SystemTrayToolsUnitTests\bin\Debug\SystemTrayToolsUnitTests.dll" /Output="C:\GitHub\eWolfApps\Reports\SystemTrayTools.html" /ReportType="HTML"

