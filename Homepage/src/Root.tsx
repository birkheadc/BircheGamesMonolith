import * as React from 'react';
import { Navigate, Route, Routes, useNavigate } from 'react-router-dom';
import Nav from './components/nav/Nav';
import LandingPage from './components/pages/landing/LandingPage';
import RegisterPage from './components/pages/register/RegisterPage';

import './styles/reset.css';
import './styles/shared.css';
import './styles/vars.css';
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

interface IRootProps {

}

/**
*
* @returns {JSX.Element | null}
*/
export default function Root(props: IRootProps): JSX.Element | null {

  const [loggedInUser, setLoggedInUser] = React.useState<IUserDTO | null | undefined>(undefined);

  const nav = useNavigate();

  React.useEffect(function checkStorageForTokenOnMount() {
    const token: string | null = localStorage.getItem(SESSION_TOKEN_KEY);
    if (token == null) {
      setLoggedInUser(null);
      return;
    }
    const payload = jwt_decode(token);
    const user = helpers.getUserFromPayload(payload);
    setLoggedInUser(user);
  }, []);

  const sendVerificationEmail = async (emailAddress: string | null) => {
    if (emailAddress != null) {
      const didSend = await api.email.requestVerificationEmail(emailAddress);
      // Todo: Maybe do something depending on whether it sent or not? For now, just pretend that it did.
    }
  }

  const login = (sessionToken: string) => {
    const payload = jwt_decode(sessionToken);
    const user = helpers.getUserFromPayload(payload);
    if (user == null) {
      // Todo: Reroute to error page or something
      return;
    }
    localStorage.setItem(SESSION_TOKEN_KEY, sessionToken);
    setLoggedInUser(user);
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
      <h1>Loading...</h1>
    )
  }

  return (
    <>
      <Nav user={loggedInUser} />
      <main>
        <Routes>
          <Route path={'/login'} element={<LoginPage login={login}/>} />
          <Route path={'/logout'} element={<LogoutPage logout={logout} />} />
          <Route path={'/email-verification/generate'} element={<GenerateVerificationEmailPage resend={sendVerificationEmail}/>} />
          <Route path={'email-verification/verify'} element={<VerifyEmailPage />} />
          <Route path={'/account/*'} element={<AccountPageRouter loggedInUser={loggedInUser} />} />
          <Route path={'/register'} element={<RegisterPage sendVerificationEmail={sendVerificationEmail} />} />
          <Route path={'/'} element={<LandingPage />}/>
          <Route path={ '*' } element={ <Navigate replace={true} to={{ pathname: '/' }} /> } ></Route>
        </Routes>
        {process.env.ENVIRONMENT != "Production" && <span style={{ textAlign: 'center', width: '100%', display: 'inline-block', padding: '2em' }}>{"Environment: " + process.env.ENVIRONMENT}</span>}
      </main>
    </>
  );
}

const SESSION_TOKEN_KEY = "SESSION_TOKEN";