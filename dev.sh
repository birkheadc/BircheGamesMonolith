#!/bin/bash

# Launches multiple services at once in dev environment

echo ""
echo ""
echo "Starting Development Environment..."
echo ""
for flag in "$@"
  do
    case "$flag" in
      all)
        echo "Launching All Services And Homepage"
        ASPNETCORE_ENVIRONMENT=Development ASPNETCORE_URLS="http://localhost:5001" bash -c 'gnome-terminal -- dotnet run --project ./Authentication/src/Authentication/Authentication.csproj'
        ASPNETCORE_ENVIRONMENT=Development ASPNETCORE_URLS="http://localhost:5002" bash -c 'gnome-terminal -- dotnet run --project ./EmailVerification/src/EmailVerification/EmailVerification.csproj'
        gnome-terminal -- bash -c 'cd ./Homepage; npm run start; exec bash'
        ASPNETCORE_ENVIRONMENT=Development ASPNETCORE_URLS="http://localhost:5003" bash -c 'gnome-terminal -- dotnet run --project ./UpdateUser/src/UpdateUser/UpdateUser.csproj'
        ASPNETCORE_ENVIRONMENT=Development ASPNETCORE_URLS="http://localhost:5000" bash -c 'gnome-terminal -- dotnet run --project ./UserRegistration/src/UserRegistration/UserRegistration.csproj'
        break
        ;;
      authentication)
        echo "Launching Authentication Service"
        ASPNETCORE_ENVIRONMENT=Development ASPNETCORE_URLS="http://localhost:5001" bash -c 'gnome-terminal -- dotnet run --project ./Authentication/src/Authentication/Authentication.csproj'
        ;;
      email-verification)
        echo "Launching Email Verification Service"
        ASPNETCORE_ENVIRONMENT=Development ASPNETCORE_URLS="http://localhost:5002" bash -c 'gnome-terminal -- dotnet run --project ./EmailVerification/src/EmailVerification/EmailVerification.csproj'
        ;;
      homepage)
        echo "Launching Homepage"
        gnome-terminal -- bash -c 'cd ./Homepage; npm run start; exec bash'
        ;;
      update-user)
        echo "Launching Update User Service"
        ASPNETCORE_ENVIRONMENT=Development ASPNETCORE_URLS="http://localhost:5003" bash -c 'gnome-terminal -- dotnet run --project ./UpdateUser/src/UpdateUser/UpdateUser.csproj'
        ;;
      user-registration)
        echo "Launching User Registration Service"
        ASPNETCORE_ENVIRONMENT=Development ASPNETCORE_URLS="http://localhost:5000" bash -c 'gnome-terminal -- dotnet run --project ./UserRegistration/src/UserRegistration/UserRegistration.csproj'
        ;;
      
    esac
  done
echo ""
echo "Finished!"
echo ""
echo ""