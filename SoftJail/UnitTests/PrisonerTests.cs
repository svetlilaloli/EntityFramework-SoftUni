namespace Tests
{
	using System;
	using System.Linq;
	using AutoMapper;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.Extensions.DependencyInjection;
	using System.Reflection;
	using NUnit.Framework;
	using SoftJail;
	using SoftJail.Data;
	using SoftJail.DataProcessor;

	[TestFixture]
	public class PrisonerTests
    {
        //Resharper disable InconsistentNaming, CheckNamespace


	
		private IServiceProvider serviceProvider;

		private static readonly Assembly CurrentAssembly = typeof(StartUp).Assembly;

		[SetUp]
		public void Setup()
		{
			Mapper.Reset();
			Mapper.Initialize(cfg => cfg.AddProfile(GetType("SoftJailProfile")));

			this.serviceProvider = ConfigureServices<SoftJailDbContext>("SoftJail");
		}

		[Test]
		public void ImportPrisonersMailsZeroTest2()
		{
			var context = serviceProvider.GetService<SoftJailDbContext>();

			var inputJson = @"[{'FullName':'Rosmunda Yoodall','Nickname':'The Lappet','Age':46,'IncarcerationDate':'18/05/1965','ReleaseDate':'19/06/2006','Bail':86810.94,'CellId':17,'Mails':[{'Description':'So here is the code. This will make it really easy to update our data.','Sender':'Billye Hakey','Address':'64 Sugar Plaza str.'},{'Description':'You know… (techno) Like The Eagles!','Sender':'Tanya Ligertwood','Address':'290 Jenna Court str.'},{'Description':'What if I find his head from another photo pointing in the right direction?','Sender':'El Done','Address':'3887 Luster Drive str.'}]},{'FullName':'Benji Ballefant','Nickname':'The Peccary','Age':38,'IncarcerationDate':'12/09/1967','ReleaseDate':'07/02/1989','Bail':93934.2,'CellId':4,'Mails':[{'Description':'Okay, I have finished my data entry for June.','Sender':'Leona Cutford','Address':'43901 Dwight Trail str.'},{'Description':'That is fine, take your time no rush. I like your work and we will like you to take your time.','Sender':'Augustine Eickhoff','Address':'6 Riverside Trail str.'}]},{'FullName':'Aguistin Rawls','Nickname':'The Sunbird','Age':25,'IncarcerationDate':'30/08/1955','ReleaseDate':'29/09/2005','Bail':90533.66,'CellId':12,'Mails':[{'Description':'I do a lot of work for local bands.','Sender':'Dynah Lawee','Address':'751 Linden Hill str.'}]}]";
			var actualOutput = Deserializer.ImportPrisonersMails(context, inputJson).TrimEnd();
			var expectedOutput = "Imported Rosmunda Yoodall 46 years old\r\nImported Benji Ballefant 38 years old\r\nImported Aguistin Rawls 25 years old";

			var assertContext = serviceProvider.GetService<SoftJailDbContext>();

			var expectedPrisonerCount = 3;
			var actualPrisonerCount = assertContext.Prisoners.Count();

			var expectedMailCount = 6;
			var actualMailCount = assertContext.Mails.Count();

			Assert.That(actualPrisonerCount, Is.EqualTo(expectedPrisonerCount),
				$"Number of inserted prisoners is incorrect!");

			Assert.That(actualMailCount, Is.EqualTo(expectedMailCount),
				"Number of inserted mails is incorrect!");

			Assert.That(actualOutput, Is.EqualTo(expectedOutput).NoClip,
				$"{nameof(Deserializer.ImportPrisonersMails)} output is incorrect!");
		}

		private static Type GetType(string modelName)
		{
			var modelType = CurrentAssembly
				.GetTypes()
				.FirstOrDefault(t => t.Name == modelName);

			Assert.IsNotNull(modelType, $"{modelName} model not found!");

			return modelType;
		}

		private static IServiceProvider ConfigureServices<TContext>(string databaseName)
			where TContext : DbContext
		{
			var services = ConfigureDbContext<TContext>(databaseName);

			var context = services.GetService<TContext>();

			try
			{
				context.Model.GetEntityTypes();
			}
			catch (InvalidOperationException ex) when (ex.Source == "Microsoft.EntityFrameworkCore.Proxies")
			{
				services = ConfigureDbContext<TContext>(databaseName, useLazyLoading: true);
			}

			return services;
		}

		private static IServiceProvider ConfigureDbContext<TContext>(string databaseName, bool useLazyLoading = false)
			where TContext : DbContext
		{
			var services = new ServiceCollection();

			services
				.AddDbContext<TContext>(
					options => options
						.UseInMemoryDatabase(databaseName)
						//.UseLazyLoadingProxies(useLazyLoading)
				);

			var serviceProvider = services.BuildServiceProvider();
			return serviceProvider;
		}
	}
}