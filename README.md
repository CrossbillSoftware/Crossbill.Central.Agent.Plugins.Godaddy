> [!WARNING]
> As soon as Godaddy has restricted the access to their API only for those who have enough domains registered, the Godaddy API used by the plugin may not work for you. Consider using [Crossbill.Central.Agent.Plugins.Cloudflare](https://github.com/CrossbillSoftware/Crossbill.Central.Agent.Plugins.Cloudflare) instead.
> The plugin's code was tested on the Godaddy API offered before that breaking changes.


# Crossbill Central plugin for Godaddy integration

Crossbill.Central.Agent.Plugins.Godaddy is the Godaddy integration plugin for Crossbill Central. The plugin allows to automatically create DNS names in Godaddy during environment provisioning using Crossbill Central's config files, Crossbill Seeder's projects or Crossbill Authority's projects. The basic usage scenario is to create an A-Record for a new user's cloud instance.

## Release version

The compiled version of the plugin can be obtained from [plugin's page on Crossbill Marketplace](https://marketplace.crossbillsoftware.com/en/Apps/Details/Crossbill.Central.Agent.Plugins.Godaddy/) either by manually downloading a compiled package or by automatically installing it using Crossbill Central.

## Common usage scenario
The common scenario is to create a DNS record in Godaddy during the provisioning of a cloud instance (in response to user request) in either AWS or GCE using Crossbill Seeder's project. The scenario is:
1. The user opens the customer-facing web site and submits a new provisioning request (either as part of a new subscription wizard or as a corporate request);
2. Crossbill Seeder processes a request by executing the actions listed in a project configuration;
3. Seeder automatically creates a new server instance in either AWS or GCE;
4. Seeder automatically deploys Crossbill Central Agent on the new server instance;
5. Seeder automatically deploys Crossbill.Central.Agent.Plugins.Godaddy plugin for Crossbill Central Agent;
6. Seeder runs Crossbill Central Agent;
7. Crossbill Central Agent receives a deployment configuration from a control server (Crossbill Seeder);
8. Central Agent automatically installs the required applications;
9. Central Agent automatically creates the DNS record in Godaddy;
10. Once all is configured, the user receives an e-mail confirmation and can start using the new server environment straight away.
The outlined scenario requires a project in Crossbill Seeder configured as outlined below.

> [!NOTE]
> The plugin can be used from the command line autonomously. Crossbill Seeder project is just a good option for the scenario automation.


## Installation
### Manual installation using Crossbill Central user interface
1. Open Crossbill Central web page in a browser (common URL for the page is `http://central.example.com` where `example.com` is the domain name provided during the product installation);
2. In the Installed Apps list (left panel), click the Crossbill Central Agent app link to see the app's plugins;
3. Enter the plugin name Crossbill.Central.Agent.Plugins.Godaddy in the search box (right panel) and click Search;
4. Click Install button;
5. Enter the plugin's settings in a popup window if required and click the Install button.

> [!NOTE]
> if the plugin did not appear in the search panel, make sure that
> 1. the Crossbill Marketplace is listed as a package source (Settings -> Package Sources)
> 2. the Central Agent is in Online mode (check the settings' value from Crossbill Central interface or see IsOnline flag in appsettings.json file located in /usr/share/CROSS/Central.Agent/appsettings.json )

### Installation using command line
1. Open a command line interface for the target server;
2. Locate a directory where Crossbill Agent is installed (by default /usr/share/CROSS/Central.Agent/ ) or the location of the Crossbill.Install utility (by default /usr/share/Crossbill.Install/ );
3. Make sure the Crossbill.Install.dll utility file is in the directory;
4. Edit an installation configuration file config.jsconf (either /usr/share/CROSS/Central.Agent/config.jsconf or /usr/share/Crossbill.Install/config.jsconf );
5. Provide the following configuration to install the plugin:
```
{
    "replacements": {
        //Central
        "@(CentralAgentPort)": "5002",
        "@(CentralAgentURL)": "http://127.0.0.1:@(CentralAgentPort)/"
    },
    "apps": [
		{
		  "Name": "Godaddy Plugin for Central Agent",
		  "AppType": "Nest",
		  "Environment": "CROSS",
		  "NestAppID": "Crossbill.Central.Agent.Plugins.Godaddy",
		  "NestSource": "Crossbill Marketplace",
		  "ParentApplication": "Crossbill.Central.Agent",
		  "CentralAgentURL": "@(CentralAgentURL)",
          "ChildrenOf": "Crossbill Central Agent"
		}
	]
}
```
6. Save changes to config.jsconf
7. Run the installation utility
```
dotnet Crossbill.Install.dll
```

### Automated installation using Crossbill Seeder's or Crossbill Authority's project
1. Open Crossbill Bone web page in a browser (common URL for the page is `http://bone.example.com` where `example.com` is the domain name provided during the product installation);
2. Go to Crossbill Seeder's project page;
3. Either create a new project or use an existing one;
4. Under the Node apps section either locate an app in the target applications' lookup or enter the configuration manually:
```
{
  "Name": "Godaddy Plugin for Central Agent",
  "AppType": "Nest",
  "Environment": "CROSS",
  "NestAppID": "Crossbill.Central.Agent.Plugins.Godaddy",
  "NestSource": "Crossbill Marketplace",
  "ParentApplication": "Crossbill.Central.Agent",
  "CentralAgentURL": "@(CentralAgentURL)",
  "Sources": [
	{
	  "ID": "Local Repository",
	  "AuthType": 3
	},
	{
	  "ID": "Crossbill Marketplace",
	  "URL": "@(AppSrc_Crossbill Marketplace_URL)",
	  "AuthType": 3
	}
  ]
}
```
5. Save the project;
6. Test the project execution by running it from the Crossbill Seeder interface or by providing a provisioning request from the customer-facing portal.

## Usage
### Add Godaddy's DNS A-record from the command line
1. Open a command line interface for the target server;
2. Locate a directory where Crossbill Agent is installed (by default /usr/share/CROSS/Central.Agent/ ) or the location of the Crossbill.Install utility (by default /usr/share/Crossbill.Install/ );
3. Make sure the Crossbill.Install.dll utility file is in the directory;
4. Edit an installation configuration file config.jsconf (either /usr/share/CROSS/Central.Agent/config.jsconf or /usr/share/Crossbill.Install/config.jsconf );
5. Provide the following configuration to run the plugin:
```
{
  "replacements": {
    
  },
  "apps": [
    {
      "Name": "Godaddy DNS config",
      "AppType": "GodaddyDNS",
      "Parameters": {
	    "AccessKey": "XXXXXXXXXXXXXX_XXXXXXXXXXXXXXXXXXXXXX",
        "SecretKey": "XXXXXXXXXXXXXXXXXXXXXX",
        "Domain": "example.com",
        "ARecords": "test,www.test",
        "IP": "XX.XXX.XXX.XX"
      }
    }
  ]
}
```
6. Save changes to config.jsconf
7. Run the installation utility
```
dotnet Crossbill.Install.dll
```

### Add Godaddy's DNS A-record from Crossbill Seeder's or Crossbill Authority's project
1. Open Crossbill Bone web page in a browser (common URL for the page is `http://bone.example.com` where `example.com` is the domain name provided during the product installation);
2. Go to Crossbill Seeder's project page;
3. Either create a new project or use an existing one;
4. Under the apps section add the configuration:
```
{
  "Name": "Godaddy DNS config",
  "AppType": "GodaddyDNS",
  "Parameters": {
	"AccessKey": "XXXXXXXXXXXXXX_XXXXXXXXXXXXXXXXXXXXXX",
	"SecretKey": "XXXXXXXXXXXXXXXXXXXXXX",
	"Domain": "example.com",
	"ARecords": "test,www.test",
	"IP": "XX.XXX.XXX.XX"
  }
}
```
5. Save the project;
6. Test the project execution by running it from the Crossbill Seeder interface or by providing a provisioning request from the customer-facing portal.

## Supported parameters
* AccessKey - the Godaddy API access key to access the DNS infrastructure;
* SecretKey - the Godaddy API secret key to access the DNS infrastructure;
* Domain - the domain name under which the subdomain will be created;
* ARecords - the comma-separated list of the subdomains that will be created;
* IP - the IP address to point the A-records to.

## License

The Crossbill Software License Agreement is located in [plugins/Crossbill.Central.Agent.Plugins.Godaddy/LICENSE.txt](plugins/Crossbill.Central.Agent.Plugins.Godaddy/LICENSE.txt) file.

The Third Party Code in Crossbill Products notice is located in [plugins/Crossbill.Central.Agent.Plugins.Godaddy/third-party-code.txt](plugins/Crossbill.Central.Agent.Plugins.Godaddy/third-party-code.txt) file.

The copyright and license texts for the third party code can be found in [plugins/Crossbill.Central.Agent.Plugins.Godaddy/third-party-notices.txt](plugins/Crossbill.Central.Agent.Plugins.Godaddy/third-party-notices.txt) file.

