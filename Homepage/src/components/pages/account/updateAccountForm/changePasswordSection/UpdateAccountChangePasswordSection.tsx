import * as React from 'react';
import './UpdateAccountChangePasswordSection.css'
import { IChangePasswordRequest } from '../../../../../types/api/requests/changePassword/changePasswordRequest';

interface IUpdateAccountChangePasswordSectionProps {

}

/**
*
* @returns {JSX.Element | null}
*/
export default function UpdateAccountChangePasswordSection(props: IUpdateAccountChangePasswordSectionProps): JSX.Element | null {

  const [ request, setRequest ] = React.useState<IChangePasswordRequest>({
    password: '',
    repeatPassword: ''
  });
  
  const handleSubmit = async (event: React.FormEvent) => {
    event.preventDefault();
    console.log("Submit update password request: ", request);
    const result = await props.submit(request);
    if (result.wasSuccess === false) {
      setMessage(result.errors.length > 0 ? result.errors[0].errorMessage : "Something went wrong...");
    } else {
      setMessage(`Success! Your display name is now ${request.displayName}#${request.tag}!`);
      setDisplayNameChosen(true);
    }
  }
  
  return (
    <form className='update-account-form-section' onSubmit={handleSubmit}>
      
    </form>
  );
}