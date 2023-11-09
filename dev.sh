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
        npm run start --prefix Homepage/ &
        ASPNETCORE_ENVIRONMENT=Development ASPNETCORE_URLS="http://localhost:5001" dotnet run --project ./Authentication/src/Authentication/Authentication.csproj &
        ASPNETCORE_ENVIRONMENT=Development ASPNETCORE_URLS="http://localhost:5002" dotnet run --project ./EmailVerification/src/EmailVerification/EmailVerification.csproj &
        ASPNETCORE_ENVIRONMENT=Development ASPNETCORE_URLS="http://localhost:5003" dotnet run --project ./UpdateUser/src/UpdateUser/UpdateUser.csproj &
        ASPNETCORE_ENVIRONMENT=Development ASPNETCORE_URLS="http://localhost:5000" dotnet run --project ./UserRegistration/src/UserRegistration/UserRegistration.csproj &
        ASPNETCORE_ENVIRONMENT=Development ASPNETCORE_URLS="http://localhost:5004" dotnet run --project ./PasswordChange/src/PasswordChange/PasswordChange.csproj &
        break
        ;;
      authentication)
        echo "Launching Authentication Service"
        ASPNETCORE_ENVIRONMENT=Development ASPNETCORE_URLS="http://localhost:5001" dotnet run --project ./Authentication/src/Authentication/Authentication.csproj &
        ;;
      email-verification)
        echo "Launching Email Verification Service"
        ASPNETCORE_ENVIRONMENT=Development ASPNETCORE_URLS="http://localhost:5002" dotnet run --project ./EmailVerification/src/EmailVerification/EmailVerification.csproj &
        ;;
      homepage)
        echo "Launching Homepage"
        npm run start --prefix Homepage/ &
        ;;
      update-user)
        echo "Launching Update User Service"
        ASPNETCORE_ENVIRONMENT=Development ASPNETCORE_URLS="http://localhost:5003" dotnet run --project ./UpdateUser/src/UpdateUser/UpdateUser.csproj &
        ;;
      user-registration)
        echo "Launching User Registration Service"
        ASPNETCORE_ENVIRONMENT=Development ASPNETCORE_URLS="http://localhost:5000" dotnet run --project ./UserRegistration/src/UserRegistration/UserRegistration.csproj &
        ;;
      password-change)
        echo "Launching Password Change Service"
        ASPNETCORE_ENVIRONMENT=Development ASPNETCORE_URLS="http://localhost:5004" dotnet run --project ./PasswordChange/src/PasswordChange/PasswordChange.csproj &
    esac
  done
echo ""
echo "Finished!"
echo ""
echo ""
trap 'pkill -P $$' SIGINT SIGTERM
wait