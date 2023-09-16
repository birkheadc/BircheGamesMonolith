import * as React from 'react';
import './LoginForm.css'
import { ICredentials } from '../../../../types/credentials/credentials';
import { ILoginResponse } from '../../../../types/sessionToken/loginResponse';

interface ILoginFormProps {
  submit: (credentials: ICredentials) => Promise<ILoginResponse>
}

/**
*
* @returns {JSX.Element | null}
*/
export default function LoginForm(props: ILoginFormProps): JSX.Element | null {

  const [credentials, setCredentials] = React.useState<ICredentials>({ emailAddress: '', password: '' });
  const [error, setError] = React.useState<string | undefined>(undefined);

  const handleChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    const name: string = event.currentTarget.name;
    const value: string = event.currentTarget.value;
    const newCredentials = {...credentials};
    newCredentials[name] = value;
    setCredentials(newCredentials);
  }

  const handleSubmit = async (event: React.FormEvent) => {
    event.preventDefault();
    (document.activeElement as HTMLElement)?.blur();
    const response = await props.submit(credentials);
    if (response.wasSuccess === false) {
      setError(response.error);
    }
  }

  return (
    <form className='login-form standard-form' onSubmit={handleSubmit}>
      <h1>Login</h1>
      {error && <span className='error'>{error}</span>}
      <div className='form-row'>
        <div className='label-input-wrapper'>
          <label htmlFor='email'>Email Address</label>
          <input id='email' type='email' name='emailAddress' onChange={handleChange} value={credentials.emailAddress}></input>
        </div>
      </div>
      <div className="form-row">
        <div className='label-input-wrapper'>
          <label htmlFor='password'>Password</label>
          <input id='password' type='password' name='password' onChange={handleChange}></input>
        </div>
      </div>
      <div className="form-row form-buttons-row">
        <button type='submit'>Submit</button>
      </div>
    </form>
  );
}