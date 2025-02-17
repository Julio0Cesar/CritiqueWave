import { useEffect, useState } from "react";
import Card from "../../components/card/Card";
import styles from "./Home.module.scss"

const Home = () => {

  return (
    <div className="container">
      <div className={styles.cards}>
          <Card/>
          <Card/>
      </div>
    </div>
  )
}

export default Home;
