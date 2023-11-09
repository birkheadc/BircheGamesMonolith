import * as React from 'react';
import './UpdateAccountChangePasswordSection.css'
import { IChangePasswordRequest } from '../../../../../types/api/requests/changePassword/changePasswordRequest';
import { IApiResponse } from '../../../../../types/api/apiResponse';
import { useNavigate } from 'react-router-dom';

interface IUpdateAccountChangePasswordSectionProps {
  
}

/**
*
* @returns {JSX.Element | null}
*/
export default function UpdateAccountChangePasswordSection(props: IUpdateAccountChangePasswordSectionProps): JSX.Element | null {

  const navigate = useNavigate();
  
  const handleSubmit = async (event: React.FormEvent) => {
    event.preventDefault();
    console.log('hi');
    navigate('/password-reset/request');
  }
  
  return (
    <form className='update-account-form-section' onSubmit={handleSubmit}>
      <h2>Change Password</h2>
      <p>If you would like to change your password, we first must reverify your email address. Press the button below to request a link to the password reset form.</p>
      <div className="form-row form-buttons-row">
        <button>Request Password Change</button>
      </div>
    </form>
  );
}