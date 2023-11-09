# TODO

## Development Environment

- Need to write tests for a lot of functionality, especially back end.
  - Email Verification does not need unit tests
  - Need to look over Update User and see if it needs unit tests
  - Integration testing needs to be done

## Other

- Change Password Function
  - Need to create a password reset service that sends a link to the user's email
  - Link leads to a form similar to email verification, with the jwt in the query string that ensures the user accessed their email
  - User then fills out the form, which posts back to the password reset service the new password with this jwt as authentication
  - Front end needs to check the query for the token on page load, and reroute if not found