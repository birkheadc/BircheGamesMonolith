import * as React from 'react';
import { Navigate, Route, Routes } from 'react-router-dom';
import AccountPage from './accountPage/AccountPage';
import { IUserDTO } from '../../../types/user/user';

interface IAccountPageRouterProps {
  token: string | null,
  setWorking: (isWorking: boolean, message: string | null) => void,
  loggedInUser: IUserDTO | null
}

/**
*
* @returns {JSX.Element | null}
*/
export default function AccountPageRouter(props: IAccountPageRouterProps): JSX.Element | null {
  return (
    <Routes>
      <Route path={'/'} element={<AccountPage token={props.token} setWorking={props.setWorking} user={props.loggedInUser} />} />
      <Route path={ '*' } element={ <Navigate replace={true} to={{ pathname: '/account' }} /> } ></Route>
    </Routes>
  );
}