# BircheGames

**This repository is from my first attempt creating for AWS. I learned a lot from it but ultimately decided to go back to the drawing board, at least as far as the back end is concerned. I've left the repository up for posterity.**

This is the monolith repo for my new site Birche Games. The site will be a place to host games I've made. Browser based games can be played here, native games can be downloaded.

User registration and user authentication APIs will also be built. Registered users can create comments, but more importantly use their account to join multiplayer games.

## Deployment

Everything is hosted on AWS.

### Creating a new service.
  - `dotnet new serverless.AspNetCoreMinimalApi -o <ServiceName>`
  - Add nuget packages
  - Modify `aws-lambda-tools-defaults.json`
    - Remove:
      - `s3-prefix`
      - `template`
      - `template-parameters`
    - Add:
      - `function-name`: `BircheGames<ServiceName>API`
      - `function-handler`: `<ServiceName>`
      - `function-role`: `arn:aws:iam::290153383648:role/BircheGames<ServiceName>_Role`
      - `function-memory-size`: `256` (Change if needed)
      - `function-timeout`: `30` (Change if needed)
  - Create IAM Role for `BircheGames<ServiceName>_Role`
    - Grant that role the permissions this API requires to function
  - Deploy the service to AWS Lambda
    - `dotnet lambda deploy-function`
    - *This is the point where I started thinking the architecture is holding me back and I should go back to the drawing board*