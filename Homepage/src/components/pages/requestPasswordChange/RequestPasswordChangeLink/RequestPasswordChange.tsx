import * as React from 'react';
import './RequestPasswordChange.css'

interface IRequestPasswordChangeProps {

}

/**
*
* @returns {JSX.Element | null}
*/
export default function RequestPasswordChange(props: IRequestPasswordChangeProps): JSX.Element | null {

  const [ request, setRequest ] = React.useState<{ emailAddress: string }>({ emailAddress: '' });

  const handleChange = (event: React.FormEvent<HTMLInputElement>) => {
    setRequest({ emailAddress: event.currentTarget.value });
  }

  const handleSubmit = (event: React.FormEvent) => {
    event.preventDefault();
    console.log("Send a password reset email to: ", request.emailAddress);
  }

  return (
    <div className='request-password-change-wrapper'>
      <form className='standard-form request-password-change-form' onSubmit={handleSubmit}>
        <h1>Change Password</h1>
        <p>Submit your email address in the form below to request a password change. A link will be sent to that address.</p>
        <div className='form-row'>
          <div className='label-input-wrapper'>
            <label htmlFor='email'>Email Address</label>
            <input id='email' type='email' name='emailAddress' onChange={handleChange} value={request.emailAddress}></input>
          </div>
        </div>
        <div className='form-row form-buttons-row'>
          <button>Submit</button>
        </div>
      </form>
      
    </div>
  );
}