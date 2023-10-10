import * as React from 'react';
import './WorkingOverlay.css'
import ReactModal from 'react-modal';
import WorkingAnimation from './workingAnimation/WorkingAnimation';

interface IWorkingOverlayProps {
  isWorking: boolean,
  message: string | null
}

/**
*
* @returns {JSX.Element | null}
*/
export default function WorkingOverlay(props: IWorkingOverlayProps): JSX.Element | null {
  return (
    <ReactModal isOpen={props.isWorking}>
      <div id='working-overlay-wrapper'>
        <div id='working-overlay-window'>
          <span>{props.message}</span>
          <WorkingAnimation />
        </div>
      </div>
    </ReactModal>
  );
}