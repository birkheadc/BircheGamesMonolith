import * as React from 'react';
import './DevPage.css'

interface IDevPageProps {
  setWorking: (isWorking: boolean, message: string | null) => void,
}

/**
*
* @returns {JSX.Element | null}
*/
export default function DevPage(props: IDevPageProps): JSX.Element | null {

  const handleTestWorkingOverlay = () => {
    props.setWorking(true, "Testing Overlay");
    setTimeout(() => {
      props.setWorking(false, null);
    }, 10000);
  }

  return (
    <div>
      <h1>Dev Test Page</h1>
      <p>This page is for hosting new in-development features. This page should not be viewable in production.</p>
      <button onClick={handleTestWorkingOverlay} type='button'>Test Working Overlay</button>
    </div>
  );
}