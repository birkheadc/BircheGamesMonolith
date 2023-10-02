import * as React from 'react';
import './AccountPage.css'
import { IUserDTO } from '../../../../types/user/user';
import { createSearchParams, useNavigate } from 'react-router-dom';
import UpdateAccountForm from '../updateAccountForm/UpdateAccountForm';
import { IUpdateUserResponse } from '../../../../types/user/updateUser/updateUserResponse';
import { IUpdateUserRequest } from '../../../../types/user/updateUser/updateUserRequest';
import api from '../../../../api';
import WorkingOverlay from '../../../shared/workingOverlay/WorkingOverlay';

interface IAccountPageProps {
  user: IUserDTO | null,
}

/**
*
* @returns {JSX.Element | null}
*/
export default function AccountPage(props: IAccountPageProps): JSX.Element | null {

  const [isWorking, setWorking] = React.useState<boolean>(false);

  const nav = useNavigate();

  React.useEffect(function navigateAwayIfUserIsNullOrNotEmailVerified() {
    if (props.user == null) nav('/');
    if (props.user?.isEmailVerified === false) {
      nav({ pathname: '/email-verification/generate', search: `?${createSearchParams({ address: props.user.emailAddress })}` });
    }
  }, [ props.user ]);

  const submitUpdateUserRequest = async (request: IUpdateUserRequest): Promise<IUpdateUserResponse> => {
    setWorking(true);
    const response = await api.user.updateUser(request);
    setWorking(false);
    return response;
  }

  return (
    <div className='account-page-wrapper'>
      <h1>Account Page</h1>
      <p>This is the account page. Only a verified user should be able to see this!</p>
      <p>If you are not logged in, this page should be completely off limits.</p>
      <p>If you are logged in, but your email is not verified, you should be redirected to a different page.</p>
      <br></br>
      { props.user && <WorkingOverlay element={<UpdateAccountForm user={props.user} submit={submitUpdateUserRequest} />} isWorking={isWorking} /> }
    </div>
  );
}