<Query Kind="Program">
  <Reference Relative="..\..\Brewmaster\brewmaster\src\Brewmaster.TemplateSDK.Contracts\bin\Debug\Brewmaster.TemplateSDK.Contracts.dll">E:\Git_Local\Brewmaster\brewmaster\src\Brewmaster.TemplateSDK.Contracts\bin\Debug\Brewmaster.TemplateSDK.Contracts.dll</Reference>
  <Namespace>Brewmaster.TemplateSDK.Contracts.Fluent</Namespace>
  <Namespace>Brewmaster.TemplateSDK.Contracts.Models</Namespace>
  <Namespace>Brewmaster.TemplateSDK.Contracts.Serialization</Namespace>
</Query>

void Main()
{
	var template = WithTemplateExtensions.CreateCompositeTemplate("TakeOne","Take One for Brewmaster Composite Template")
										.WithPackage("es","af817d05-6307-5095-a2f8-854482fe788e","master")
										.WithPackage("arr","2fdab0e1-365b-5aca-831f-a950e0b1ebe2","master")
										.WithParameterMap("ARRServerNamePrefix","es","ServerNamePrefix")
										.WithParameterMap("ESServerNamePrefix","web","ServerNamePrefix");
										
	//Elastic Search Parameters
	template = template.WithParameter("Region", ParameterType.String, "Name of Azure region.", "AzureRegionName")
                .WithParameter("AffinityGroup", ParameterType.String, "Name of Azure affinity group.",
                               "AzureAffinityGroupName")
                .WithParameter("CloudService", ParameterType.String, "Name of the Azure Cloud Service.",
                               "AzureCloudServiceName")
                .WithParameter("DiskStore", ParameterType.String, "Name of Azure disk storage account.",
                               "AzureStorageName")
                .WithParameter("VMSize", ParameterType.String, "Size of the server VMs.", "AzureRoleSize",
                               p => p.WithDefaultValue("Small"))
                .WithParameter("VmAdminName", ParameterType.String, "Name of local administrator account.mapped", "username",
                               p => p.WithLimits(1, 64))
                .WithParameter("AdminPassword", ParameterType.String, "Password of local administrator account.",
                               "password",
                               p => p.WithLimits(8, 127), maskValue: true)
                .WithParameter("ServerNamePrefix", ParameterType.String, "Name prefix for ElasticSearch servers.",
                               p => p.WithDefaultValue("esn")
                                     .WithRegexValidation(@"^[a-zA-Z][a-zA-Z0-9-]{1,13}$",
                                                          "Must contain 3 to 14 letters, numbers, and hyphens. Must start with a letter."))
				.WithParameter("AzureCertificateUrl", ParameterType.String, "URL to Azure certificate.",
                               "string",
                               p => p.WithLimits(8, 127))
				.WithParameter("AzureCertificatePassword", ParameterType.String, "Password for the certificate file.",
                               "password",
                               p => p.WithLimits(8, 127), maskValue: true)										  
				.WithParameter("AzureSubscriptionId", ParameterType.String, "Subscription Id.",
                               "Guid",
                               p => p.WithLimits(8, 127))			
                .WithParameter("NumberOfElasticSearchServers", ParameterType.Number, "Number of ElasticSearch servers.", "integer",
                               p => p.WithDefaultValue("2")
                                     .WithLimits(2, 100)
                                     .WithRegexValidation(@"^\d+$", "Must enter a positive integer between 2 and 100."))
				.WithParameter("DataDiskSize", ParameterType.Number, "Size of Data disk(GB).", "integer",
                               p => p.WithDefaultValue("100")
                                     .WithLimits(2, 1024)
                                     .WithRegexValidation(@"^\d+$", "Must enter a positive integer between 2 and 1024."))
				.WithParameter("NumberOfWebServers", ParameterType.Number, "Number of ARR servers.", "integer",
                               p => p.WithDefaultValue("2")
                                     .WithLimits(2, 100)
                                     .WithRegexValidation(@"^\d+$", "Must enter a positive integer between 2 and 100."));
		
	template.Save(@"E:\Git_Local\Brewmaster.CompositeOne\Brewmaster.CompositeOne\");
	//template.ToJson().Dump();

}

// Define other methods and classes here