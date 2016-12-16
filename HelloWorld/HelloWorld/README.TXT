HelloWorld.ConsoleWriter is the entrypoint into the application. It depends on HelloWorld.BusObj, which provides 
the business classes for the Writer. The Program.cs file demonstrates how to initialize a ConsoleWriter and use it

HelloWorld.DatabaseWriter is another ConsoleApplication that depends on the HelloWorld.BusObj class. It demostrates
how you would extend the base classes in a /separate/ project to develop a different application (ie. one that writes
to a database) assuming you didn't have access to HelloWorld.BusObj.