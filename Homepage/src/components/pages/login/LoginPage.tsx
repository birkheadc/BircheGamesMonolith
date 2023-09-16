import * as React from 'react';
import './LoginPage.css'
import LoginForm from './loginForm/LoginForm';
import { ICredentials } from '../../../types/credentials/credentials';
import api from '../../../api';

interface ILoginPageProps {
  login: (sessionToken: string) => void
}

/**
*
* @returns {JSX.Element | null}
*/
export default function LoginPage(props: ILoginPageProps): JSX.Element | null {

  const [working, setWorking] = React.useState<boolean>(false);

  const handleLogin = async (credentials: ICredentials) => {
    setWorking(true);
    const response = await api.authentication.retrieveSessionToken(credentials);
    setWorking(false);

    if (response.wasSuccess) {
      // Todo: store the session token in local storage and reroute to /account page probably (this code should be in Root.tsx)
    }
    return response;
  }

  return (
    <div className='login-page-wrapper'>
      <LoginForm submit={handleLogin} />
    </div>
  );
}