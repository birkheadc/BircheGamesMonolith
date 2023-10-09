import * as React from 'react';
import './LoginPage.css'
import LoginForm from './loginForm/LoginForm';
import { ICredentials } from '../../../types/credentials/credentials';
import api from '../../../api';
import WorkingOverlay from '../../shared/workingOverlay/WorkingOverlay';

interface ILoginPageProps {
  login: (sessionToken: string) => void
}

/**
*
* @returns {JSX.Element | null}
*/
export default function LoginPage(props: ILoginPageProps): JSX.Element | null {

  const [isWorking, setWorking] = React.useState<boolean>(false);

  const handleLogin = async (credentials: ICredentials) => {
    setWorking(true);
    const response = await api.authentication.retrieveSessionToken(credentials);
    setWorking(false);

    if (response.wasSuccess && response.sessionToken) {
      props.login(response.sessionToken);
    }
    
    return response;
  }

  return (
    <div className='login-page-wrapper'>
      <WorkingOverlay element={<LoginForm submit={handleLogin} />} isWorking={isWorking} />
    </div>
  );
}