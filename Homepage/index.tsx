import * as React from "react";
import { createRoot } from "react-dom/client";
import Root from "./src/Root";
import { BrowserRouter } from "react-router-dom";

import Modal from 'react-modal';
import { UserProvider } from "./src/contexts/userContext/UserContext";

Modal.setAppElement('#react-root');
Modal.defaultStyles.content = {};
Modal.defaultStyles.overlay = {
  ...Modal.defaultStyles.overlay,
  backgroundColor: 'rgba(0, 0, 0, 0.4)',
  zIndex: 5
};

const container = document.getElementById("react-root");
if (container != null) {
  const root = createRoot(container);
  root.render(<BrowserRouter><UserProvider><Root /></UserProvider></BrowserRouter>);
}