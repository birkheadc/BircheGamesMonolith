import * as React from 'react';
import './UserRegisterForm.css'
import INewUser from '../../../../types/user/newUser/newUser';

interface IUserRegisterFormProps {
  submit: (user: INewUser) => void
}

/**
*
* @returns {JSX.Element | null}
*/
export default function UserRegisterForm(props: IUserRegisterFormProps): JSX.Element | null {

  const [user, setUser] = React.useState<INewUser>({
    emailAddress: '',
    displayName: '',
    tag: '',
    password: '',
    confirmPassword: ''
  });

  const handleSubmit = (event: React.FormEvent) => {
    event.preventDefault();
    console.log(event);
  }

  const handleChange = (event: React.ChangeEvent<HTMLInputElement>) => {

  }

  return (
    <form className='standard-form' onSubmit={handleSubmit}>
      <h1>Create Account</h1>
      <div className='form-row'>
        <div className='label-input-wrapper'>
          <label htmlFor='email'>Email Address</label>
          <input id='email' type='email' name='emailAddress' onChange={handleChange} value={user.emailAddress}></input>
        </div>
      </div>
      <div className='form-row gap-small'>
        <div className='label-input-wrapper'>
          <label htmlFor='display-name-base'>Display Name</label>
          <input className='text-align-right' id='display-name-base' name='displayName' onChange={handleChange} value={user.displayName}></input>
        </div>
        <span className='display-name-hash-tag-span'>#</span>
        <div className='label-input-wrapper display-name-tag-wrapper'>
          <label htmlFor='display-name-tag'>Tag</label>
          <input id='display-name-tag' name='tag' onChange={handleChange} value={user.tag}></input>
        </div>
      </div>
      <div className='form-row gap-large'>
        <div className='label-input-wrapper'>
          <label htmlFor='password'>Password</label>
          <input id='password' type='password' name='' onChange={handleChange}></input>
        </div>
        {/* <span className='display-name-hash-tag-span hidden'>#</span> */}
        <div className='label-input-wrapper'>
          <label htmlFor='password-repeat'>Repeat Password</label>
          <input id='password-repeat' type='password' onChange={handleChange}></input>
        </div>
      </div>
      <div className="form-row form-buttons-row">
        <button type='submit'>Submit</button>
      </div>
    </form>
  );
}