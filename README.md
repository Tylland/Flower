# Flower
## Get Started
### Create a Flower

First thing we need to do i s to create a flow logger
~~~~
 var flower = new FlowerConfiguration()
                .WriteTo.File(@"c:\temp\flows.log")
                .CreateFlower();
 ~~~~
 
 Then we can start logging! This is done by first creating a flow configuration and then call the Seed method.
 
 ~~~~
  var flowConfig = new FlowConfiguration
            {
                StepNames = new[] { "First", "Second", "Third" },
                Name = "Test Group",
                FinishTimeout = TimeSpan.FromSeconds(10),
                StepTimeout = TimeSpan.FromSeconds(20)
            };
            
flower.Seed("0001", flowConfig);
 ~~~~

By calling Seed the first step in the flow is registred. We register the other steps in the flow with Feed method:

~~~~
flower.Feed("0001", "Second");
flower.Feed("0001", "Third");
~~~~
