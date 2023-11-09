import * as React from 'react';
import { Navigate, Route, Routes } from 'react-router-dom';
import RequestPasswordChange from './RequestPasswordChangeLink/RequestPasswordChange';
import PasswordChange from './PasswordChange/PasswordChange';

interface IRequestPasswordChangePageRouterProps {

}

/**
*
* @returns {JSX.Element | null}
*/
export default function RequestPasswordChangePageRouter(props: IRequestPasswordChangePageRouterProps): JSX.Element | null {
  return (
    <Routes>
      <Route path={'/request'} element={<RequestPasswordChange />} />
      <Route path={'/change'} element={<PasswordChange />} />
      <Route path={'*'} element={ <Navigate replace={true} to={{ pathname: '/' }}/> } />
    </Routes>
  );
}