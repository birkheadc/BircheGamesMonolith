import * as React from 'react';
import './VerifyEmailPage.css'
import { useSearchParams } from 'react-router-dom';
import api from '../../../api';

interface IVerifyEmailPageProps {

}

/**
*
* @returns {JSX.Element | null}
*/
export default function VerifyEmailPage(props: IVerifyEmailPageProps): JSX.Element | null {

  const [status, setStatus] = React.useState<string>("Processing...");

  const [params] = useSearchParams();
  const code = params.get('code');

  React.useEffect(() => {
    (async function processVerificationCode() {
      const result: { wasSuccess: boolean, error?: string | undefined } = await api.email.verifyCode(code);
      setStatus(result.wasSuccess ? "Congratulations, your account has been verified. You may now login and use your new account." : result.error ?? "Something went wrong...");
    })();
  }, [ code ]);

  return (
    <div className='verify-email-page-wrapper'>
      <p>{status}</p>
    </div>
  );
}