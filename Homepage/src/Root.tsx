import * as React from 'react';
import { Navigate, Route, Routes, useNavigate } from 'react-router-dom';
import Nav from './components/nav/Nav';
import LandingPage from './components/pages/landing/LandingPage';
import RegisterPage from './components/pages/register/RegisterPage';

import './styles/reset.css';
import './styles/shared.css';
import './styles/vars.css';
import './styles/fonts.css';
import AccountPage from './components/pages/account/accountPage/AccountPage';
import { IUserDTO } from './types/user/user';
import LoginPage from './components/pages/login/LoginPage';

import jwt_decode from 'jwt-decode';
import helpers from './helpers';
import api from './api';
import GenerateVerificationEmailPage from './components/pages/emailVerification/GenerateVerificationEmailPage';
import VerifyEmailPage from './components/pages/emailVerification/VerifyEmailPage';
import LogoutPage from './components/pages/logout/LogoutPage';
import AccountPageRouter from './components/pages/account/AccountPageRouter';
import { IApiResponse } from './types/api/apiResponse';
import DevPage from './components/pages/dev/DevPage';
import WorkingOverlay from './components/workingOverlay/WorkingOverlay';

interface IRootProps {

}

/**
*
* @returns {JSX.Element | null}
*/
export default function Root(props: IRootProps): JSX.Element | null {

  const [isWorking, setWorking] = React.useState<{ isWorking: boolean, message: string | null }>({ isWorking: false, message: null });
  const [loggedInUser, setLoggedInUser] = React.useState<IUserDTO | null | undefined>(undefined);

  const nav = useNavigate();

  React.useEffect(() => {
    (async function checkStorageForTokenOnMount() {
      const token: string | null = localStorage.getItem(SESSION_TOKEN_KEY);
      if (token == null) {
        console.log("Could not find token to login automatically");
        setLoggedInUser(null);
        return;
      }
      setWorking({
        isWorking: true,
        message: "Loading"
      });
      const response: IApiResponse<IUserDTO> = await api.user.getCurrentUser(token);
      if (response.wasSuccess === false || response.body == null) {
        // Todo: Reroute to error page or something
        setLoggedInUser(null);
        setWorking({ isWorking: false, message: null });
        return;
      }
      localStorage.setItem(SESSION_TOKEN_KEY, token);
      setLoggedInUser(response.body);
      setWorking({ isWorking: false, message: null });
    })();
  }, []);

  const sendVerificationEmail = async (emailAddress: string | null) => {
    if (emailAddress != null) {
      const didSend = await api.email.requestVerificationEmail(emailAddress);
      // Todo: Maybe do something depending on whether it sent or not? For now, just pretend that it did.
    }
  }

  const login = async (sessionToken: string) => {
    const response: IApiResponse<IUserDTO> = await api.user.getCurrentUser(sessionToken);
    if (response.wasSuccess === false || response.body == null) {
      // Todo: Reroute to error page or something
      return;
    }
    localStorage.setItem(SESSION_TOKEN_KEY, sessionToken);
    setLoggedInUser(response.body);
    nav('account');
  }

  const logout = () => {
    localStorage.removeItem(SESSION_TOKEN_KEY);
    setLoggedInUser(null);
    nav('/');
  }

  if (process.env.ENVIRONMENT === 'Production') return (
    <h1>Birche Games is still under development!</h1>
  )

  if (loggedInUser === undefined) {
    return (
      <></>
    )
  }

  const handleSetWorking = (isWorking: boolean, message: string | null) => {
    setWorking({ isWorking, message });
  }

  return (
    <>
      <WorkingOverlay isWorking={isWorking.isWorking} message={isWorking.message} />
      <Nav user={loggedInUser} />
      <main>
        <Routes>
          <Route path={'/dev'} element={<DevPage setWorking={handleSetWorking} />} />
          <Route path={'/login'} element={<LoginPage setWorking={handleSetWorking} login={login}/>} />
          <Route path={'/logout'} element={<LogoutPage logout={logout} />} />
          <Route path={'/email-verification/generate'} element={<GenerateVerificationEmailPage resend={sendVerificationEmail}/>} />
          <Route path={'email-verification/verify'} element={<VerifyEmailPage />} />
          <Route path={'/account/*'} element={<AccountPageRouter loggedInUser={loggedInUser} />} />
          <Route path={'/register'} element={<RegisterPage setWorking={handleSetWorking} sendVerificationEmail={sendVerificationEmail} />} />
          <Route path={'/'} element={<LandingPage />}/>
          <Route path={ '*' } element={ <Navigate replace={true} to={{ pathname: '/' }} /> } ></Route>
        </Routes>
        {process.env.ENVIRONMENT != "Production" && <span style={{ textAlign: 'center', width: '100%', display: 'inline-block', padding: '2em' }}>{"Environment: " + process.env.ENVIRONMENT}</span>}
      </main>
    </>
  );
}

const SESSION_TOKEN_KEY = "SESSION_TOKEN";