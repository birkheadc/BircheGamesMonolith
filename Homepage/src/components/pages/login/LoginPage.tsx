import * as React from 'react';
import './LoginPage.css'
import LoginForm from './loginForm/LoginForm';
import { ICredentials } from '../../../types/credentials/credentials';
import api from '../../../api';
import helpers from '../../../helpers';

interface ILoginPageProps {
  setWorking: (isWorking: boolean, message: string | null) => void,
  login: (sessionToken: string) => void
}

/**
*
* @returns {JSX.Element | null}
*/
export default function LoginPage(props: ILoginPageProps): JSX.Element | null {

  const handleLogin = async (credentials: ICredentials) => {
    props.setWorking(true, "Logging In");
    const response = await api.authentication.retrieveSessionToken(credentials);

    if (response.wasSuccess && response.sessionToken) {
      await props.login(response.sessionToken);
    }

    props.setWorking(false, null);
    return response;
  }

  return (
    <div className='login-page-wrapper'>
      <LoginForm submit={handleLogin} />
    </div>
  );
}