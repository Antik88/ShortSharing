import * as React from 'react';
import Box from '@mui/material/Box';
import Tab from '@mui/material/Tab';
import TabContext from '@mui/lab/TabContext';
import TabList from '@mui/lab/TabList';
import TabPanel from '@mui/lab/TabPanel';
import OwnerStepFierst from '../assets/owner_step1.svg';
import OwnerStepSecond from '../assets/owner_step2.svg';
import OwnerStepTherd from '../assets/owner_step3.svg';
import OwnerStepFors from '../assets/owner_step4.svg';
import StepsTenant from '../assets/steps_tenant.svg';
import { Fade, Grid2, Slide } from '@mui/material';

type TabValue = '1' | '2';

export default function HowToUse() {
  const [value, setValue] = React.useState<TabValue>('1');

  const handleChange = (_event: React.SyntheticEvent, newValue: TabValue) => {
    setValue(newValue);
  };

  return (
    <Fade in={true} timeout={1300}>
      <Box sx={{
        typography: 'body1',
        mr: 3,
        height: '35vh',
        border: '0'
      }}>
        <TabContext value={value}>
          <Box sx={{
            display: 'flex',
            flexDirection: 'column',
            justifyContent: 'right',
            alignItems: 'flex-end'
          }}>
            <TabList onChange={handleChange} sx={{
              '& .MuiTab-root': {
                color: 'primary.light',
              },
              '& .Mui-selected': {
                color: 'primary.main',
              },
              '& .MuiTabIndicator-root': {
                backgroundColor: 'primary.main',
              }
            }}>
              <Tab
                label="Owner"
                sx={{ textTransform: 'none' }}
                value="1" />
              <Tab
                label="Tenant"
                sx={{ textTransform: 'none' }}
                value="2" />
            </TabList>
            <TabPanel value="1">
              <Slide direction="right" in={value === '1'} mountOnEnter unmountOnExit>
                <Grid2 container spacing={2} sx={{ justifyContent: 'center' }}>
                  <img src={OwnerStepFierst} alt="Step 1" />
                  <img src={OwnerStepSecond} alt="Step 2" />
                  <img src={OwnerStepTherd} alt="Step 3" />
                  <img src={OwnerStepFors} alt="Step 4" />
                </Grid2>
              </Slide>
            </TabPanel>
            <TabPanel value="2">
              <Slide direction="right" in={value === '2'} mountOnEnter unmountOnExit>
                <img src={StepsTenant} alt="Steps Tenant" />
              </Slide>
            </TabPanel>
          </Box>
        </TabContext>
      </Box>
    </Fade>
  );
}
