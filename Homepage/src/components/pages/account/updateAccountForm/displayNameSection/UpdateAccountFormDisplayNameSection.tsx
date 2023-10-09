import * as React from 'react';
import './UpdateAccountFormDisplayNameSection.css'
import { IUpdateDisplayNameRequest } from '../../../../../types/api/requests/updateUser/updateDisplayNameRequest';
import { IApiResponse } from '../../../../../types/api/apiResponse';

interface IUpdateAccountFormDisplayNameSectionProps {
  submit: (request: IUpdateDisplayNameRequest) => Promise<IApiResponse>
}

/**
*
* @returns {JSX.Element | null}
*/
export default function UpdateAccountFormDisplayNameSection(props: IUpdateAccountFormDisplayNameSectionProps): JSX.Element | null {

  const [request, setRequest] = React.useState<IUpdateDisplayNameRequest>({
    displayName: '',
    tag: ''
  });

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
    console.log("Submit display name change request: ", request);
    props.submit(request);
  }

  return (
    <form className='update-account-form-section' onSubmit={handleSubmit}>
      <h2>Display Name</h2>
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