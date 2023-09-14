import * as React from 'react';
import './WorkingOverlay.css'

interface IWorkingOverlayProps {
  isWorking: boolean,
  element: React.ReactElement
}

/**
*
* @returns {JSX.Element | null}
*/
export default function WorkingOverlay(props: IWorkingOverlayProps): JSX.Element | null {

  return (
    <div className='working-overlay-wrapper'>
      <div style={ props.isWorking ? elementWorkingStyle : elementNotWorkingStyle }>
        {props.element}
      </div>
      <div className='working-spinner-wrapper'>
        <div className='working-spinner' style={ props.isWorking ? spinnerWorkingStyle : spinnerNotWorkingStyle }></div>
      </div>
    </div>
  );
}

const elementWorkingStyle: React.CSSProperties = {
  opacity: 0.3,
  pointerEvents: 'none'
}

const elementNotWorkingStyle: React.CSSProperties = {
  opacity: 1.0,
  pointerEvents: 'all'
}

const spinnerWorkingStyle: React.CSSProperties = {
  opacity: 1.0
}

const spinnerNotWorkingStyle: React.CSSProperties = {
  opacity: 0.0
}