import * as React from 'react';
import './AccountPage.css'

interface IAccountPageProps {

}

/**
*
* @returns {JSX.Element | null}
*/
export default function AccountPage(props: IAccountPageProps): JSX.Element | null {
  return (
    <div className='account-page-wrapper'>
      <h1>Account Page</h1>
    </div>
  );
}