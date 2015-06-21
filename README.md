CoolWhip [![Build status](https://ci.appveyor.com/api/projects/status/ew7ya7ebm5ohgg4d/branch/master?svg=true)](https://ci.appveyor.com/project/jquintus/coolwhip/branch/master) [![NuGet version](https://badge.fury.io/nu/CoolWhip.svg)](https://www.nuget.org/packages/CoolWhip/)
====================

The coolest way to whip out NuGet releases from GitHub and [AppVeyor](http://www.appveyor.com/).  

CoolWhip creates a default AppVeyor configuration.  

Continuous Integration
------------------------

On check in AppVeyor will build your project and run tests.


Continuous Deployment
----------------------

When a GitHub release or git tag is created

1. Your project is built
1. Your tests are run
1. All NuGet packages are packaged and 
    1. Attached to the release in GitHub
    1. Pushed to NuGet
    1. Pushed to [Symbol Source](http://symbolsource.org/)

Getting Started
====================

Overview
-------------------

1. Install CoolWhip `PM> Install-Package CoolWhip`
2. Encrypt GitHub key
3. Encrypt NuGet key
4. Update the appveyor.yaml with the keys from above
5. Save/commit changes
6. Link AppVeyor to your repo





Special Thanks To
====================

* [AppVeyor](http://www.appveyor.com/): #1 Continuous Delivery service for Windows
* Robert Daniel Moore for his [NuGet Test Harness](https://robdmoore.id.au/blog/2013/08/07/test-harness-for-nuget-install-powershell-scripts-init-ps1-install-ps1-uninstall-ps1/) also on [GitHub](https://github.com/robdmoore/NuGetCommandTestHarness)
* NuGet for being awesome
