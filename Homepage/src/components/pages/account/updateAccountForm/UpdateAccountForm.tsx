import * as React from 'react';
import './UpdateAccountForm.css'
import { IUserDTO } from '../../../../types/user/user';
import { IUpdateUserRequest } from '../../../../types/user/updateUser/updateUserRequest';
import { IUpdateUserResponse } from '../../../../types/user/updateUser/updateUserResponse';

interface IUpdateAccountFormProps {
  user: IUserDTO,
  submit: (request: IUpdateUserRequest) => Promise<IUpdateUserResponse>
}

/**
*
* @returns {JSX.Element | null}
*/
export default function UpdateAccountForm(props: IUpdateAccountFormProps): JSX.Element | null {

  const user = props.user;

  const [ request, setRequest ] = React.useState<IUpdateUserRequest>({
    displayName: user.displayName,
    tag: user.tag
  });

  const [ error, setError] = React.useState<string | undefined>(undefined);

  React.useEffect(function clearDisplayNameAndTagIfNotYetChosen() {
    if (user.isDisplayNameChosen === false) {
      setRequest(r => ({ ...r, displayName: '', tag: '' }));
      setError('Create a display name and tag that your friends can find you by!');
    }
  }, [ user ]);

  const handleChange = (event: React.FormEvent<HTMLInputElement>) => {
    const name = event.currentTarget.name;
    const value = event.currentTarget.value;
    setRequest(r => {
      const newRequest = {...r};
      newRequest[name] = value;
      return newRequest;
    })
  }

  const handleSubmit = (event: React.FormEvent) => {
    event.preventDefault();
    console.log("Submit: ", request);
    props.submit(request);
  }

  return (
    <form className='update-account-form standard-form' onSubmit={handleSubmit}>
      <h1>Account Information</h1>
      {error && <span className='error'>{error}</span>}
      <div className='form-row'>
        <div className='label-input-wrapper disabled'>
          <label htmlFor='email'>Email Address</label>
          <input disabled={true} id='email' type='email' name='emailAddress' value={user.emailAddress}></input>
        </div>
      </div>
      <div className='form-row'>
      <div className='label-input-wrapper'>
          <label htmlFor='display-name'>Display Name</label>
          <input className='text-align-right' id='display-name' name='displayName' onChange={handleChange} value={request.displayName}></input>
        </div>
        <span className='display-name-hash-sign'>#</span>
        <div className='label-input-wrapper'>
          <label htmlFor='tag'>Tag</label>
          <input id='tag' name='tag' onChange={handleChange} value={request.tag}></input>
        </div>
      </div>
      <div className="form-row form-buttons-row">
        <button type='submit'>Submit</button>
      </div>
    </form>
  );
}