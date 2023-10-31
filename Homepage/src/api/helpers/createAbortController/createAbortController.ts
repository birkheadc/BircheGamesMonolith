import config from "../../../config";

export default function createAbortSignal(): { signal: AbortSignal, timeout: NodeJS.Timeout } {
  const controller = new AbortController();
  const timeout = setTimeout(() => {
    controller.abort();
  }, config.general.apiCallTimeout);
  return { signal: controller.signal, timeout };
}