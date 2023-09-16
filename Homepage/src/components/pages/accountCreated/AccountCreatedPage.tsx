import * as React from 'react';
import './AccountCreatedPage.css'
import { Link, useSearchParams } from 'react-router-dom';

interface IAccountCreatedPageProps {

}

/**
*
* @returns {JSX.Element | null}
*/
export default function AccountCreatedPage(props: IAccountCreatedPageProps): JSX.Element | null {

  const [params] = useSearchParams();
  const emailAddress = params.get('address');

  const handleResend = () => {

  }

  return (
    <div className='account-created-page-wrapper'>
      {emailAddress == null || emailAddress === '' ?
        <>
          <p>Something has gone wrong...</p>
        </> :
        <>
          <h1>Your account is not yet created!</h1>
          <p>We've sent an email to <span className='bold'>{emailAddress}</span> with instructions on how to finalize your account.</p>
          <p>If you haven't received the message yet, you can press the button below to request another.</p>
          <p>If <span className='bold'>{emailAddress}</span> is not the correct address, please fill out the <Link to={'/register'}>registration form</Link> again with the correct address</p>
          <button type='button' onClick={handleResend}>Resend</button>
        </>
      }
    </div>
  );
}