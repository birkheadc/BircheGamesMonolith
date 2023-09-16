import * as React from 'react';
import './AccountPage.css'
import { IUserDTO } from '../../../types/user/user';

interface IAccountPageProps {
  user: IUserDTO | null
}

/**
*
* @returns {JSX.Element | null}
*/
export default function AccountPage(props: IAccountPageProps): JSX.Element | null {
  return (
    <div className='account-page-wrapper'>
      <h1>Account Page</h1>
      <p>This is the account page. Only a verified user should be able to see this!</p>
      <p>If you are not logged in, this page should be completely off limits.</p>
      <p>If you are logged in, but your email is not verified, you should be redirected to a different page.</p>
    </div>
  );
}