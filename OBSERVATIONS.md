# Observations

### Most important issues:
- security
    - the user password is return on get request
    - the user password is not kept encrypted on in the database
    - the app returns the errors that code throws back to the user in plain text with all the call stack
    - the app does not have any authorization so anyone can get and query the database
    - the get requests also include the address. This is a bit security and legal problem
    - the user does not need to add an email address to be verified
- structure
    - All the app is in one project
    - there is no way for the user to authenticate, update, or delete his profile
    - the app/controller does not respect the rest structure.
    - in controller we inject the repository directly
    - in UserManager the repository call is too over engineered and we should not create new task with Task.FromResult and get the result from the task with .Result

### All in one architecture
The project created using all in one architecture. This is an older concept that can be improve by using clean code architecture. 
Also there are not unit tests added to the solution

## On user creation
The user "login" is not unique this means that we can create 2 users with the same login. This is a big issue as the second user with the same "login" can't be found by login/username
## The backend errors are displayed in the browsers
the errors that are thrown by the backend are display in plan text into swagger. This is a big security issue as a potential attaker could see the errors and use them in his attack. The erros should never be display outside of or app. They should be logged and logged.

## View models
In UserRequest class
- the class knows to much for a registration. From a registion I think we should have only Password, email and maybe username, firstname and lastName the rest of the fields should be added after the user confirms his identity(by clicking on a link from an email sent to him)
- we need to add a regex for the password to help the user to have a stronger password
- we need to add a new field for email adress and validate it when a new user is created
- change the field Login to UserName as "Login" is not a easy to understand name. 

### Entities

As all the entities we have have by default an Id and CreatedAt and UpdatedAt I suggest to put those into a class and then all entities classes that required Id to inherit from it. this will provide conosistency.

On AddressEntity
- there is no need to add [DatabaseGenerated(DatabaseGeneratedOption.Identity)] for Id fields marked as key. The database gerated is and explicit and the key attribute is already doing that.
- the  values for Contry, state and City I would keep them as int values and connected with their tables like City, Country, and State and expose some enpoints to get the values. This way the data is more structured.

On UserEntitty
-  using aspnet identity will help us creatae the identity, password hashing and structure. We can extend the identity and add extra fields that we need 
-  the password should be hashed, not plain text and not encrypted.

### Controllers
 - Is a mixt of calling the UserRepository and the UserManager interface  from user controller. I would first stick to one as using 2 will only create confusion. Then we should never user repository from controllers this just add business logic to the controller and will brake the separation of concerns principal. In conclusion al the call to database should be done only from the core layer or in this case the UserManager.
 - Since we use swagger adding some nice summaries to the controller would be helpfull so the clients will understand this easy.
 - there is no authorizion for getting users from database this is a security consern as anyone can query our database without having anypermission. 
 - on get calls we return all the user object and address/user info here we have 2 problems
    -  the password is returned in plain text
    -  the user address is return. I think this is a sensitive information and should not be displayed without user consent.
- the app does not respect the rest we should not have 3 get endpoints but only one and diffrent query params. The struture of the api url should be like /api/ControllerName.
 - to get user address we should have another controller that returns the address for the requested userID

### UserManager
- this user manager does not respect the single responsability principal as it save the user and the address. I think this 2 are diffrent entitties and should be done in diffrent services so the user service should not know how to save the address. It may call the address service
- the repository calls are over engineered and will result in a dead locks. The ".Result" should be avoided as much as possible and use async/await
- using async/await will result in no need to create a new thread with Task.FromResult
- on user create since Address is referenced from UserEntity is not need to save the entity in db first than put it in user object. They can be done all in one roundtrip by adding the address object to the user than the user can be saved  also saving the corresponding address.

### Startup
- ApplicationDbContext is injected into configure method and used to migrated the database to a new version after all the configurations are applied by the middleware. We should do this after we register the dbContext
- UseQueryTrackingBehavior should be used for read-only databases to make it faster but if we want to also update the user we should remove it
- for a better visiblity we should create one or to functions to inject the repositories and services.

### Code formating and usings?
I know that this may not be important but having a solution that respects a defined struture will be easy to use and undestand. The namespace and the project name are not the same

# Proposed solution

- create 3 solutions  for identity and one for user profile and user notifications/emails
- user clean architecture for each
- add exception middleware
- use message broker
- create docker files

### Implemented changes
- added authorization and authentication(jwt generation)
- logging using Nlog was added
- middleware for exception handle
- restructure the Identity solution to follow the clean architeacture
- reorganize the code to respect restAPI pattern
- change the UserEntity to inherit from UserIdenity
- remove AddressEntity as it should be in profile service along with other properties that were on UserEntity
- udpate swagger to have authorization capabilities