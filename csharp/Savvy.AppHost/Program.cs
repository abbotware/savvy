var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.Savvy_ZooKeeper>("savvy-zookeeper");

builder.Build().Run();
