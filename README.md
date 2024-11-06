# SocialMediaTesting

## Overview
This is just a prototype for our UMNX SocialMedia App. Was just created to test feasibility so we can either build off of this or start from scratch and just use this as guidance. 
The code is still pretty rough and likely not following best design practices or consistency. Again everything was done relatively fast just to test functionality. Much of the main 
display window is still pure HTML as I was not focused on design when making what exists. There are two projects within the solution, SocialMediaTesting, and SocialMediaAPI. You can
ignore the SocialMediaAPI for now as it's not fully setup yet. When complete that will allow connection to the server, but the program currently uses localhost to save and retrieve data.

## Current Features
- User Login/Logout
- Account Creation
- Search for Existing Users
- Upload Text Posts
- View Individual Profiles
- View Post and User Details (Following, Followers, Number of Likes On a Post, Following Status)

## Prerequisites
Make sure you have the following installed on your machine:
- [.NET SDK](https://dotnet.microsoft.com/download) (version 8.0)
- [Visual Studio](https://visualstudio.microsoft.com/downloads/) or [Rider](https://www.jetbrains.com/rider/download/) (for development)
- There may be additional NuGet packages it will ask you to install after the project is open. Make sure to install the ones it says you need. 

## Getting Started

### Clone the Repository
1. To get a local copy of the repository, navigate to the directory you want the project in locally (E.g. C:\Projects) and run the following command:
```bash
git clone https://github.com/your-username/SocialMediaTesting.git
```
2. Open the .sln file in Visual Studio or Rider.  
3. Build the entire solution.
4. Once built, select the SocialMediaTesting http configuration and run using the green play button.  
5. It should open the web app to the home page running on your localhost and the url should look something like "localhost:1234" (If this doesn't work please let me know).  

Because it uses local storage you won't have any user data by default so you will have to manually make a few accounts to test out the features and functionality.
