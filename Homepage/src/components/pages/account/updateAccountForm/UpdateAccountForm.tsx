import * as React from 'react';
import './UpdateAccountForm.css'
import { IUserDTO } from '../../../../types/user/user';
import UpdateAccountFormDisplayNameSection from './displayNameSection/UpdateAccountFormDisplayNameSection';
import { IUpdateDisplayNameRequest } from '../../../../types/api/requests/updateUser/updateDisplayNameRequest';
import { IApiResponse } from '../../../../types/api/apiResponse';

interface IUpdateAccountFormProps {
  user: IUserDTO,
  updateDisplayName: (request: IUpdateDisplayNameRequest) => Promise<IApiResponse>
}

/**
*
* @returns {JSX.Element | null}
*/
export default function UpdateAccountForm(props: IUpdateAccountFormProps): JSX.Element | null {

  const user = props.user;

  const [ error, setError] = React.useState<string | undefined>(undefined);

  return (
    <div className='update-account-form standard-form'>
      <h1>Account Information</h1>
      {error && <span className='error standard-form-message'>{error}</span>}
      <div className='update-account-form-section'>
        <h2>Contact Information</h2>
        <div className='form-row'>
          <div className='label-input-wrapper disabled'>
            <label htmlFor='email'>Email Address</label>
            <input disabled={true} id='email' type='email' name='emailAddress' value={user.emailAddress}></input>
          </div>
        </div>
      </div>
      <UpdateAccountFormDisplayNameSection isDisplayNameChosen={props.user.isDisplayNameChosen} request={{ displayName: props.user.displayName, tag: props.user.tag}} submit={props.updateDisplayName}/>
    </div>
  );
}