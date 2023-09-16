import registration, { IRegistrationConfig } from "./registration";
import general, { IGeneralConfig } from "./general";

interface IConfig {
  registration: IRegistrationConfig,
  general: IGeneralConfig
}

const config: IConfig = {
  registration,
  general
}

export default config;