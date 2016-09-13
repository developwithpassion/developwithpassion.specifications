# developwithpassion.specifications

developwithpassion.specifications is a lightweight utility layer on top of the following 2 fantastic libraries:

* [Machine.Specifications](https://github.com/machine/machine.specifications)
* [Machine.Fakes](https://github.com/BjRo/Machine.Fakes)

With the help of Machine.Fakes it currently can be used to do interaction based testing with the following faking libraries:

* [FakeItEasy](https://github.com/FakeItEasy/FakeItEasy)
* [Moq](https://github.com/Moq/moq)
* [Rhino.Mocks](https://github.com/ayende/rhino-mocks)
* [NSubstitute](https://github.com/nsubstitute/NSubstitute)

Thanks to the Machine teams and the Fakes team for the contributions that this library heavily leverages.

## Contributing code

* [Get a github account](https://github.com/signup/free)
* [Login to github](https://github.com/login)
* Fork the main [repository](https://github.com/developwithpassion/developwithpassion.specifications)
* Push changes to your fork
* Send me a pull request

# Getting Started


## Building from source

* Make sure you have the .Net Framework 4.0 installed on your machine.

Clone the repository from github and run the following command at a command prompt:

* build.cmd dist:zip

If the build does not work, please submit an issue through a pull request. After a succesful build you will find all of the zip files under the distribution/zips folder. Currently only 5 of the zip files created are important:

* developwithpassion.specifications.zip - Contains the core library
* developwithpassion.specifications.rhinomocks.zip - Contains the adapters for Machine.Fakes.RhinoMocks
* developwithpassion.specifications.fakeiteasy.zip - Contains the adapters for Machine.Fakes.FakeItEasy
* developwithpassion.specifications.nsubstitute.zip - Contains the adapters for Machine.Fakes.NSubstitute
* developwithpassion.specifications.moq.zip - Contains the adapters for Machine.Fakes.Moq

## Getting developwithpassion.specifications via NuGet

Installing with NuGet is a snap. There are currently 4 packages available on NuGet (one for each of the Machine.Fakes adapter libraries). To use with Rhino.Mocks for example, just launch the package manager console and type:

* install-package developwithpassion.specifications.rhinomocks

This will install all of the necessary dependencies for the library. The other packages that you could choose are:

* developwithpassion.specifications.fakeiteasy
* developwithpassion.specifications.moq
* developwithpassion.specifications.nsubstitute

## How to use it

The core of the functionality in developwithpassion.specifications can be accessed by deriving your test classes from one of the following 3 classes:

* Observes
* Observes< ClassToTest >
* Observes< Contract , Class >

The last 2 classes provide access to automatic creation of the system under test.

The [developwithpassion.specifications.examples](https://github.com/developwithpassion/developwithpassion.specifications/tree/master/source/developwithpassion.specifications.examples) project contains many examples of how to use different features of the library. Feel free to contribute other examples as you see fit.

[Develop With Passion®](http://www.developwithpassion.com)
