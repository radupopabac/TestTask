# ASP.NET Core Test Task

We really appreciate your time and effort spent on this exercise!

Here is what we would like you to do:
1. Review the current solution for any design and implementation problems.
2. Write down all your observations.
3. Refactor the existing code to make it easier to maintain, scale and extend. 

## General overview

* This application is going to be heavy loaded so it should scale out very well. As such, resources should be used efficiently. 
* You may notice that the code is quite error-prone in certain places. Think about possible ways of improvement.
* The code should never leave the database in a corrupted state.
* User Entity is an aggregate root. It is going to be extended to have more child entities later on (e.g. Phone Numbers, Document IDs). All of those don't exist without a user.
* Please note that multiple people will be working on this solution simultaneously. Its structure should allow for it.
* The code should be easy to maintain and test.
* Application's API endpoints will be white-labeled (i.e. lots of third parties will be integrating with it). As such, best practices for REST API design should be considered.  

## How to capture your observations

Please create OBSERVATIONS.md file and write down all your comments. Try to keep them concise and clear.

## Code refactoring

You can make whatever changes you need in order to fulfill the requirements listed above. This might include adding new projects, moving some code into those, changing/merging API endpoints, leveraging useful NuGet packages of your choice or anything else.

There is no need to cover the code with unit/integration tests. Though, putting together one or two tests will be a plus.

We don't want you to spend an excessive amount of time on this so if in doubt, please ask. If you realize that this task takes longer than 2-4 hours, please collect all your ideas/suggestions along with your observations.

Good luck and, most importantly, have fun! :)

## How to publish the results

You can make all the changes in a separate branch. Once you are done, you can create a pull request and provide us with a URL to it (or a pull request number). OBSERVATIONS.md file is supposed to be a part of the pull request.

if you run into issues with pull request creation, please feel free to create a new public GitHub repository with all of your changes and send us a link to it.