# Python Selenium Tests Interoperability

## Usage

This project contains a basic Python interoperability framework for coded automations, as well as an example of a wrapped Selenium test written in Python, which we additionally augmented with some UiPath Platform services. You can also choose to run bare-bones/unaltered Selenium tests.
The approach showcases how you can use the coded automations framework in order to either:

1. Gradually transition your Selenium tests to UiPath test cases.
2. Run Selenium test cases but have reporting, monitoring, parallelization and verification handled through UiPath's platform.
3. Leverage UiPath activity packages within Selenium tests (e.g. UI Automation for scenarios which Selenium can't handle) or run custom UiPath workflows within them.

## Requirements

1. Install IronPython 3.4 from [here](https://github.com/IronLanguages/ironpython3/releases/download/v3.4.0/IronPython-3.4.0.msi) (requires Administrator rights).
2. Add the path `C:\Program Files\IronPython 3.4` to your environment variables (see [here](https://linuxhint.com/add-directory-to-path-environment-variables-windows/) for a guide), or open an elevated command prompt, e.g. using WIN+X -> Powershell (Admin) and cd into `C:\Program Files\IronPython\3.4`. Non-elevated command prompts will result in errors when running the below commands.
3. Run the below commands in the command prompt:
- `ipy -X:Frames -m ensurepip`
- `ipy -X:Frames -m pip install selenium`

4. Open Chrome, click the three dots menu in the top right, go to Help -> About and note the Chrome version.
5. Go [here](https://chromedriver.chromium.org/downloads) and download the chromedriver version corresponding to your major Chrome version (e.g. latest chromedriver.exe v114.x if you have Chrome v114.x).
6. Extract the chromedriver ZIP file and copy `chromedriver.exe` into an environment variable in your `PATH`, e.g. the recently added `C:\Program Files\IronPython 3.4` or another one that you know is available.
7. Open the below project in Studio and run `TestCase_SeleniumTest`, which showcases a couple of usages.
