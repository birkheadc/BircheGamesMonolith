import * as React from 'react';
import { BrowserRouter, Navigate, Route, Routes, createSearchParams, useNavigate } from 'react-router-dom';
import Nav from './components/nav/Nav';
import LandingPage from './components/pages/landing/LandingPage';
import RegisterPage from './components/pages/register/RegisterPage';

import './styles/reset.css';
import './styles/shared.css';
import './styles/vars.css';
import AccountPage from './components/pages/account/AccountPage';
import AccountCreatedPage from './components/pages/accountCreated/AccountCreatedPage';
import { IUserDTO } from './types/user/user';
import { env } from 'process';
import config from './config';

interface IRootProps {

}

/**
*
* @returns {JSX.Element | null}
*/
export default function Root(props: IRootProps): JSX.Element | null {

  console.log("Create User API Url: ", config.registration.apiUrl);

  const [loggedInUser, setLoggedInUser] = React.useState<IUserDTO | null>(null);

  const sendVerificationEmail = (address: string) => {
    
  }

  return (
    <BrowserRouter>
      <Nav />
      <main>
        <Routes>
          <Route path={'/account-created'} element={<AccountCreatedPage />} />
          <Route path={'/account'} element={loggedInUser ? <AccountPage user={null} /> : <Navigate replace={true} to={{ pathname: '/' }} /> } />
          <Route path={'/register'} element={<RegisterPage sendVerificationEmail={sendVerificationEmail} />} />
          <Route path={'/'} element={<LandingPage />}/>
          <Route path={ '*' } element={ <Navigate replace={true} to={{ pathname: '/' }} /> } ></Route>
        </Routes>
      </main>
    </BrowserRouter>
  );
}