import { AppBar, Button, Grid, Tab, Tabs, Typography } from "@mui/material";
import { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import useRouteMatch from "./hooks/useRouteMatch";
import { getAsync } from "./axios";

const init_user = {
  isAuth: false
}

const Navigation = () => {
  const [user, setUser] = useState(init_user);

  useEffect(() => {

    (async () => {
      const { isOk, data } = await getAsync("https://localhost/api/users/info",)
      isOk && setUser({ ...data, isAuth: true });
    })()

  }, []);

  const routeMatch = useRouteMatch(["/", "/rooms", "/itemsLibrary", "/user/unbording"]);
  const currentTab = routeMatch?.pattern?.path;
  return (
    <AppBar position="static">
      <Grid
        container
        direction="row"
        justifyContent="space-between"
        alignItems="center"
      >
        <Grid item sx={{ ml: 1 }}>
          <Grid container spacing={1}>
            <Grid item>
              <Tabs
                value={currentTab}
                textColor="white"
                TabIndicatorProps={{
                  style: {
                    backgroundColor: "#F6BCFF",
                  },
                }}
                sx={{ mb: 0.5 }}
              >
                <Tab label="Home" value="/" to="/" component={Link} />
                <Tab
                  label="Rooms"
                  value="/rooms"
                  to="/rooms"
                  component={Link}
                />
                <Tab
                  label="Items library"
                  value="/itemsLibrary"
                  to="/itemsLibrary"
                  component={Link}
                />
                <Tab
                  label="Unbording test"
                  value="/user/unbording"
                  to="/user/unbording"
                  component={Link}
                />
              </Tabs>
            </Grid>
          </Grid>
        </Grid>
        <Grid item sx={{ mr: 1 }}>
          {user.isAuth ? (
            <Typography>{user?.userName}</Typography>
          ) : (
            <Button
              color="inherit"
              to="/user/login"
              component={Link}
            >Login</Button>
          )}
        </Grid>
      </Grid>
    </AppBar>
  );
};

export default Navigation;
