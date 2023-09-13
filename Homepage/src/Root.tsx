import * as React from 'react';
import { BrowserRouter, Navigate, Route, Routes } from 'react-router-dom';
import Nav from './components/nav/Nav';
import LandingPage from './components/pages/landing/LandingPage';
import RegisterPage from './components/pages/register/RegisterPage';

import './styles/reset.css';
import './styles/shared.css';
import './styles/vars.css';

interface IRootProps {

}

/**
*
* @returns {JSX.Element | null}
*/
export default function Root(props: IRootProps): JSX.Element | null {
  return (
    <BrowserRouter>
      <Nav />
      <main>
        <Routes>
          <Route path={'/register'} element={<RegisterPage />} />
          <Route path={'/'} element={<LandingPage />}/>
          <Route path={ '*' } element={ <Navigate replace={true} to={{ pathname: '/' }} /> } ></Route>
        </Routes>
      </main>
    </BrowserRouter>
  );
}