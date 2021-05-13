# Using the TaxBandits API with C#
***
TaxBandits API SDK written on C# in .Net Core framework to show how to integrate with TaxBandits API. This covers the following API endpoints:
### Form941
- Create 
- Get 
- Transmit
- List
- Delete
- Status
### Business
- Create 
- Get
- List
- Delete
## Configuration
 You need to signup with TaxBandits Sandbox Developer Console at https://sandbox.taxbandits.com/User/Register to get the keys to run
the SDK. See below for more directions:
### To get the sandbox keys:
- Go to Sandbox Developer console: https://sandbox.taxbandits.com
- Signup or signin to Sandbox 
- Navigate to settings and then to API Credentials. Copy client id, client secret and user token. 
### The sandbox URLs: (Please make sure to use the right versions)
- Sandbox Auth Server: https://testoauth.expressauth.net/v2/tbsauth 
- API Server: https://testapi.taxbandits.com/v1.6.1 
- Sandbox Application URL: https://testapp.taxbandits.com 
### How to use?
Below are the steps to integrate with TaxBandits API:
1. Sign up for a new developer account in https://sandbox.taxbandits.com/User/Register
2. Navigate to **settings** from the left menu and choose **API Credentials**
3. You can find your Client Id, Client Secret and User Token in the API Credentials page.
4. Open appsettings.json file and replace with your Client Id, Client Secret and User Token under appsettings tag.
5. Play with the sample application provided for sandbox testing.
### Project Folder Structure
* controllers:
    - The users input data are parsed and request models are constructed here.
    - All API response validations are also done here.
    - UI rendering logics (example, the dropdown values and default value mappings) are also included in the controllers.   
* models:
    - This folder contains the request and response models of all the API end points.
* Views:
    - All our view pages (.cshtml) are placed under this folder. 
* appsettings.json:
    - The appsettings.json file is an application configuration file used to add custom keys.
* wwwroot:
    - The wwwroot folder is a default folder in the ASP.NET Core project is treated as a web root folder. 
    - Static files can be stored in any folder under the web root and accessed  with a relative path to that root.
* Program.cs:
    - A console file which starts executing from the entry point public static void Main() in Program class where we can create a host for the web application.
* Startup.cs:
    - A class file, it is like Global.asax in the traditional .NET application and it is executed first when the application starts.
# Our Articles on API Integration
 Please refer the following link for the complete API documentation that covers all the API methods with their sample request and response.
 https://developer.taxbandits.com/docs/
For more detailed instructions on integrating with TaxBandits API, refer https://taxbanditsdev.medium.com/
### Contact Details
   Email: developer@taxbandits.com  