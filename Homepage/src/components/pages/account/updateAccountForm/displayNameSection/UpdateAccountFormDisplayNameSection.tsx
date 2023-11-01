import * as React from 'react';
import './UpdateAccountFormDisplayNameSection.css'
import { IUpdateDisplayNameRequest } from '../../../../../types/api/requests/updateUser/updateDisplayNameRequest';
import { IApiResponse } from '../../../../../types/api/apiResponse';

interface IUpdateAccountFormDisplayNameSectionProps {
  submit: (request: IUpdateDisplayNameRequest) => Promise<IApiResponse>
  isDisplayNameChosen: boolean,
  request: IUpdateDisplayNameRequest
}

/**
*
* @returns {JSX.Element | null}
*/
export default function UpdateAccountFormDisplayNameSection(props: IUpdateAccountFormDisplayNameSectionProps): JSX.Element | null {

  const [isDisplayNameChosen, setDisplayNameChosen] = React.useState<boolean>(props.isDisplayNameChosen);
  const [request, setRequest] = React.useState<IUpdateDisplayNameRequest>({
    displayName: '',
    tag: ''
  });

  const [ message, setMessage ] = React.useState<string | undefined>(undefined);

  React.useEffect(function clearDisplayNameAndTagIfNotYetChosen() {
    if (props.isDisplayNameChosen === false) {
      setRequest({ displayName: '', tag: '' });
      setMessage('Create a display name and tag that your friends can find you by!');
    } else {
      setRequest(props.request);
    }
  }, [ props.isDisplayNameChosen ]);

  const handleChange = (event: React.FormEvent<HTMLInputElement>) => {
    const name = event.currentTarget.name;
    const value = event.currentTarget.value;
    setRequest(r => {
      const newRequest = {...r};
      newRequest[name] = value;
      return newRequest;
    })
  }

  const handleSubmit = async (event: React.FormEvent) => {
    event.preventDefault();
    console.log("Submit display name change request: ", request);
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
      {message && <span className='error standard-form-message'>{message}</span>}
      <h2>Display Name</h2>
      <div className='form-row'>
      <div className={`label-input-wrapper${isDisplayNameChosen ? ' disabled' : ''}`}>
          <label htmlFor='display-name'>Display Name</label>
          <input disabled={isDisplayNameChosen} className='text-align-right' id='display-name' name='displayName' onChange={handleChange} value={request.displayName}></input>
        </div>
        <span className='display-name-hash-sign'>#</span>
        <div className={`label-input-wrapper${isDisplayNameChosen ? ' disabled' : ''}`}>
          <label htmlFor='tag'>Tag</label>
          <input disabled={isDisplayNameChosen} id='tag' name='tag' onChange={handleChange} value={request.tag}></input>
        </div>
      </div>
      <div className="form-row form-buttons-row">
        <button disabled={isDisplayNameChosen} type='submit'>Submit</button>
      </div>
    </form>
  );
}