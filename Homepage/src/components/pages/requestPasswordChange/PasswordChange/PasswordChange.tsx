import * as React from 'react';
import './PasswordChange.css'

interface IPasswordChangeProps {

}

/**
*
* @returns {JSX.Element | null}
*/
export default function PasswordChange(props: IPasswordChangeProps): JSX.Element | null {

  const [ request, setRequest ] = React.useState<{ password: string, repeatPassword: string }>({
    password: '',
    repeatPassword: ''
  });

  const handleChange = (event: React.FormEvent<HTMLInputElement>) => {
    // Todo: setRequest, validate password length / both fields match (use registration form as reference)
  }

  const handleSubmit = (event: React.FormEvent) => {
    event.preventDefault();
    console.log("Submit request: ", request);
  }

  return (
    <div className='password-change-wrapper'>
      <form className='standard-form password-change-form' onSubmit={handleSubmit}>
        <h1>Change Password</h1>
        <p>You may change your password with the form below.</p>
        <div className='form-row'>
          <div className='label-input-wrapper'>
            <label htmlFor='password'>Password</label>
            <input id='password' name='password' onChange={handleChange} value={request.password}></input>
          </div>
          <div className='label-input-wrapper'>
            <label htmlFor='repeat-password'>Repeat Password</label>
            <input id='repeat-password' name='repeatPassword' onChange={handleChange} value={request.repeatPassword}></input>
          </div>
        </div>
        <div className='form-row form-buttons-row'>
          <button>Submit</button>
        </div>
      </form>
    </div>
  );
}